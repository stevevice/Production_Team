﻿using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.Events;
using UnityStandardAssets.Utility;

public class UnitAttributes : MonoBehaviour
{

    public class PlayerEvent : UnityEvent
    {

    }

    public static PlayerEvent playerDeath;

    public float health;                    //How far away is the Unit from being Destroyed
    [HideInInspector]
    public List<GameObject> weaponsList;    //List of all the Wepaons
    [HideInInspector]
    public GameObject currentWeapon;        //Weapon Currently Using
    public GameObject deathParticles;

    //To caculate speed
    float preTime;      //Previous time
    Vector3 preVector;  //previous Vector
    float force;        //How much force is that object carring

    //Race Manager Variables
    [HideInInspector]
    public int lap;                 //Current lap
    [HideInInspector]
    public int checkPoints;         //Checkpoint Number
    [HideInInspector]
    public Checkpoint nextPoint;    //The next Checkpoint
    public int placeValue;          //Number for sorting
    public bool display;

    // Use this for initialization
    void Start()
    {
        placeValue = 0;
        //nextPoint = checkPointsList.items[0];
        preTime = 0;     //Set Pretime   
        preVector = gameObject.transform.position;  //Set Prevector

        if (display == false)
        {
            int childCount = gameObject.transform.childCount;   //Get Number of children
            for (int i = 0; i < childCount; i++)
            {
                if (transform.GetChild(i).gameObject.tag == "Weapon")    //If a weapon
                {
                    transform.GetChild(i).gameObject.SetActive(false);  //Set acticve to false
                    weaponsList.Add(transform.GetChild(i).gameObject);  //Add weapon
                }
            }

            if (childCount > 0) //if we have Weapons
            {
                currentWeapon = weaponsList[0]; //Current weapon is the first one
                currentWeapon.SetActive(true);  //Turn on that object
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Player")) //If the Player
        {
            ChangeWeapon();
        }
    }

    void FixedUpdate()
    {
        if (health <= 0f)    //if has no health
        {
            gameObject.SetActive(false);    //Destroy Game Object
            Instantiate(deathParticles, transform.position, new Quaternion());
        }

        if (gameObject.transform.position.y < 0)    //if Unit falls off the track
        {
            if(gameObject.tag == "Player" && gameObject.GetComponent<Player_Move>().enabled == true)  //If they are the player
            {
                //Set to last checkpoint
                CheckPointHighlight cpH = gameObject.GetComponent<CheckPointHighlight>();
                if (cpH.checkList.IndexOf(cpH.unitAt.gameObject) - 1 >= 0)
                {
                    transform.position = cpH.checkList[cpH.checkList.IndexOf(cpH.unitAt.gameObject) - 1].transform.position;
                    transform.LookAt(cpH.checkList[cpH.checkList.IndexOf(cpH.unitAt.gameObject)].transform);
                }

                else
                {
                    transform.position = cpH.checkList[0].transform.position;
                    transform.LookAt(cpH.checkList[cpH.checkList.IndexOf(cpH.unitAt.gameObject)].transform);
                }


                gameObject.GetComponent<Player_Move>().speed = 0;
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3();
                
            }

            else if(gameObject.tag == "Player" && gameObject.GetComponent<Player_Move>().enabled == false)
            {
                //Set to last checkpoint
                CheckPointHighlight cpH = gameObject.GetComponent<CheckPointHighlight>();
                if (cpH.checkList.IndexOf(cpH.unitAt.gameObject) - 1 >= 0)
                {
                    transform.position = cpH.checkList[cpH.checkList.IndexOf(cpH.unitAt.gameObject) - 1].transform.position;
                    transform.LookAt(cpH.checkList[cpH.checkList.IndexOf(cpH.unitAt.gameObject)].transform);
                }

                else
                {
                    transform.position = cpH.checkList[0].transform.position;
                    transform.LookAt(cpH.checkList[cpH.checkList.IndexOf(cpH.unitAt.gameObject)].transform);
                }
            }

            //If AI Unit, set to position on track
            else
            {
                gameObject.transform.position = gameObject.GetComponent<WaypointProgressTracker>().progressPoint.position;
                transform.LookAt(gameObject.GetComponent<AI_Movement>().target);
                gameObject.GetComponent<AI_Movement>().speed = 0;
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3();
            }

            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3();
            transform.LookAt(nextPoint.transform);

        }

        float timeInt = Time.time - preTime;                        //Interval of Time
        Vector3 dist = gameObject.transform.position - preVector;   //Change in position

        force = dist.sqrMagnitude / timeInt;    //How hard the Unit will hit
        preTime = Time.time;                    //set pretime to current time
        preVector = gameObject.transform.position;  //Set preVector to current Vector
    }

    void OnCollisionEnter(Collision other)
    {   //Do i collide with a weapon
        if (other.gameObject.tag == "Weapon")
        {
            if (other.gameObject.transform.parent != null)
            {
                float dam = other.gameObject.GetComponent<Weapons>().damage;                        //Get Damage of Weapon
                float otherForce = other.gameObject.GetComponentInParent<UnitAttributes>().force;   //Get force it is moving at

                if (Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) <= .25f && Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) >= -.25f)     //if other is facing the side of the Unit
                {
                    health -= dam * otherForce;     //Apply normal dammage
                    if(other.transform.parent.gameObject.tag == "Player")                   
                        other.transform.parent.gameObject.GetComponent<Player_Move>().speed /= 2;
                    else if(other.transform.parent.gameObject.tag == "Unit")
                        other.transform.parent.gameObject.GetComponent<AI_Combat>().speed /= 2;
                }
                    

                else if (Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) > .25f)
                    health -= Mathf.Abs((dam * otherForce) - (force / Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward))); //Number will be positive, so take a little force off.

                else if (Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) < -.25f)
                {
                    health -= Mathf.Abs((dam * otherForce) - (force / Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward))); //Minus because of the negative dot product, minus a negative to add
                    if (other.transform.parent.gameObject.tag == "Player")
                        other.transform.parent.gameObject.GetComponent<Player_Move>().speed = 0;
                    else if (other.transform.parent.gameObject.tag == "Unit")
                        other.transform.parent.gameObject.GetComponent<AI_Combat>().speed = 0;
                }
                    
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") && other.gameObject.GetComponent<Bullet_Control>().unitFired != gameObject)
        {
            Bullet_Control otherScript = other.gameObject.GetComponent<Bullet_Control>();

            if (Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) <= .25f && Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) >= -.25f)    //if other is facing the side of the Unit
                health -= Mathf.Abs(otherScript.damage * otherScript.force);   //Apply normal dammage

            else if (Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) > .25f)
                health -= Mathf.Abs((otherScript.damage * otherScript.force) - (force / Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward))); //Number will be positive, so take a little force off.

            else if (Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) < -.25f)
                health -= Mathf.Abs((otherScript.damage * otherScript.force) - (force / Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward))); //Minus because of the negative dot product, minus a negative to add
        }
    }

    public void ChangeWeapon() //Way for the Unit to change weapons
    {
        currentWeapon.SetActive(false);     //Set Current to false
        if (weaponsList.IndexOf(currentWeapon) + 1 >= weaponsList.Count) //if last weapon
        {
            currentWeapon = weaponsList[0]; //Change to first
        }

        else
        {
            currentWeapon = weaponsList[weaponsList.IndexOf(currentWeapon) + 1];    //Change to next
        }
        currentWeapon.SetActive(true);  //Set new current weapon's active to true
    }
}