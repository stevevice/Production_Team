using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

public class UnitAttributes : MonoBehaviour {

    float health;
    public List<Weapons> weaponsList;

    //To caculate speed
    float preTime;
    Vector3 preVector;
    float force;

	// Use this for initialization
	void Start () {
        preTime = 0;
        preVector = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float timeInt = Time.time - preTime;
        Vector3 dist = gameObject.transform.position - preVector;

        force = dist.sqrMagnitude / timeInt;
        preTime = Time.time;
        preVector = gameObject.transform.position;
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Weapon"))
        {
            float dam = other.gameObject.GetComponent<Weapons>().damage;
            Debug.Log(dam);
            //other.gameObject.GetComponentInParent<>();
            
            //health -= dam + ;
        }
    }
}
