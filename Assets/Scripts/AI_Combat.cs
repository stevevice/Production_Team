using UnityEngine;
using System.Collections.Generic;

public class AI_Combat : MonoBehaviour {

    //Bullet Variables
    [SerializeField] GameObject bullet;         //Bullet Prefab
    [SerializeField] GameObject bulletspawn;    //Bullet spawn location
    AudioSource cannonFire;                     //Audio for firing
    float nextfire;                             //Next time the cannon can fire
    public float firerate;                      //Rate at which the cannon can fire

    //General Variable
    public float maxSpeed;  //The fastest possible speed the Unit can go
    public float speed;     //Current speed of the Unit
    public float acceleration;  //Rate of how fast the Unit will increace in speed.
    Vector3 fwd;    //Forward of the Unit scaled

    //AI Variables
    GameObject target;  //Unit that will be targeted

    void Start () {
        FindNewTarget();    //Find a target

        fwd = transform.forward / 100;  //Scale the Transform for moving
        nextfire = 0f;                  //Set the next fire 
        cannonFire = gameObject.GetComponent<AudioSource>();    //Get the Audio source for the cannon
	}
	
	void Update () {
        if (Time.time >= nextfire)      //Requirements to fire a bullet
            FireBullet();

        if (speed >= maxSpeed)  //Applying speed
            speed = maxSpeed;
        else
            speed += acceleration;

        //Behaviors go here

        TurnUnit();
        MoveUnit();
	}

    void FireBullet()   //So AI can fire a bullet. Will not go through the Cannon Gameobject
    {
        GameObject temp;
        temp = Instantiate(bullet, bulletspawn.transform.position, new Quaternion()) as GameObject; //Make Bullet
        cannonFire.Play();  //Play Cannon firew Sound
        temp.GetComponent<Bullet_Control>().unitFired = gameObject;                 //Set the unit that fired the bullet
        temp.GetComponent<Bullet_Control>().SetForce(transform.forward * speed);    //Give Bullet force greater than the Unit     

        nextfire = Time.time + firerate;    //Set next fire
    }

    void MoveUnit()
    {
        if (speed >= maxSpeed)  //If going faster than max speed
        {
            transform.Translate(fwd * maxSpeed, Space.Self);    //move at max speed
        }

        else       //else if not going at max speed
        {
            transform.Translate(fwd * speed, Space.Self);   //move at speed
        }
    }

    void TurnUnit() //Function designed to turn the unit
    {
        Vector3 dist = target.transform.position - gameObject.transform.position;
        Debug.Log(dist);
    }

    void FindNewTarget()    //Function to find a new target. Based on closest Unit.
    {
        //Get all Units
        List<GameObject> viableTargets = new List<GameObject>();
        foreach (GameObject u in GameObject.FindGameObjectsWithTag("Unit"))
            viableTargets.Add(u);
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            viableTargets.Add(p);

        //Compare Their Distances
        float minDis = Mathf.Infinity;
        GameObject tempTarget = null;

        foreach(GameObject g in viableTargets)
        {
            float dist = (g.transform.position - gameObject.transform.position).sqrMagnitude;
            if(dist < minDis)
            {
                minDis = dist;
                tempTarget = g;
            }
        }

        target = tempTarget;    //Set new Target
    }
}
