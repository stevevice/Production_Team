using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

public class UnitAttributes : MonoBehaviour {

    public float health;                    //How far away is the Unit from being Destroyed
    public List<GameObject> weaponsList;    //List of all the Wepaons
    public GameObject currentWeapon;        //Weapon Currently Using

    //To caculate speed
    float preTime;      //Previous time
    Vector3 preVector;  //previous Vector
    float force;        //How much force is that object carring

	// Use this for initialization
	void Start () {
        preTime = 0;     //Set Pretime   
        preVector = gameObject.transform.position;  //Set Prevector

        int childCount = gameObject.transform.childCount;   //Get Number of children
        for(int i = 0; i < childCount; i++)
        {
            if(transform.GetChild(i).gameObject.tag == "Weapon")    //If a weapon
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Player"))
        {
            currentWeapon.SetActive(false);
            if(weaponsList.IndexOf(currentWeapon) + 1 >= weaponsList.Count)
            {
                currentWeapon = weaponsList[0];
            }

            else
            {
                currentWeapon = weaponsList[weaponsList.IndexOf(currentWeapon) + 1];
            }
            currentWeapon.SetActive(true);
        }
    }

    void FixedUpdate () {
        if(health <= 0f)
        {
            Destroy(gameObject);
        }

        float timeInt = Time.time - preTime;
        Vector3 dist = gameObject.transform.position - preVector;

        force = dist.sqrMagnitude / timeInt;
        preTime = Time.time;
        preVector = gameObject.transform.position;
	}

    void OnCollisionEnter(Collision other)
    {   //Do i collide with a weapon
        if(other.gameObject.tag == "Weapon")
        {
            if (other.gameObject.transform.parent != null) {
                float dam = other.gameObject.GetComponent<Weapons>().damage;
                float otherForce = other.gameObject.GetComponentInParent<UnitAttributes>().force;

                if (Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) <= .25f && Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) >= -.25f)     
                    health -= dam * otherForce;

                else if (Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) > .25f)
                    health -= (dam * otherForce) - (force / Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward)); //Number will be positive, so take a little force off.

                else if(Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) < -.25f)
                    health -= (dam * otherForce) - (force / Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward)); //Minus because of the negative dot product, minus a negative to add
            }
        }
        //Is the object a bullet and is it not my bullet
        else if (other.gameObject.CompareTag("Bullet") && other.gameObject.GetComponent<Bullet_Control>().unitFired != gameObject)
        {
            Bullet_Control otherScript = other.gameObject.GetComponent<Bullet_Control>();

            if (Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) <= .25f && Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) >= -.25f)
                health -= otherScript.damage * otherScript.force;

            else if (Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) > .25f)
                health -= (otherScript.damage * otherScript.force) - (force / Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward)); //Number will be positive, so take a little force off.

            else if (Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward) < -.25f)
                health -= (otherScript.damage * otherScript.force) - (force / Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward)); //Minus because of the negative dot product, minus a negative to add
            Destroy(other.gameObject);
        }
    }
}
