using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

public class UnitAttributes : MonoBehaviour {

    public float health;                           //How far away is the Unit from being Destroyed
    public List<GameObject> weaponsList;    //List of all the Wepaons
    public GameObject currentWeapon;        //Weapon Currently Using

    //To caculate speed
    float preTime;      //Previous time
    Vector3 preVector;
    float force;

	// Use this for initialization
	void Start () {
        preTime = 0;        
        preVector = gameObject.transform.position;

        int childCount = gameObject.transform.childCount;   //Get Number of children
        for(int i = 0; i < childCount; i++)
        {
            if(transform.GetChild(i).gameObject.tag == "Weapon")    //If a weapon
            {
                transform.GetChild(i).gameObject.SetActive(false);
                weaponsList.Add(transform.GetChild(i).gameObject);
            }
        }

        if (childCount > 0)
        {
            currentWeapon = weaponsList[0];
            currentWeapon.SetActive(true);
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
    {
        if(other.gameObject.tag == "Weapon")
        {
            if (other.gameObject.transform.parent != null) {
                float dam = other.gameObject.GetComponent<Weapons>().damage;
                float otherForce = other.gameObject.GetComponentInParent<UnitAttributes>().force;

                health -= dam + otherForce;
            }
        }
    }
}
