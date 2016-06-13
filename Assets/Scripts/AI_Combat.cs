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
    public enum MethodOfAttack
    {
        Spear,
        Projectile,
        Mixed,
    }

    GameObject target;                  //Unit that will be targeted
    public MethodOfAttack attackMethod; //How will the Unit Behave


    void Start () {
        FindNewTarget();    //Find a target

        fwd = transform.forward / 100;  //Scale the Transform for moving
        nextfire = 0f;                  //Set the next fire 
        cannonFire = gameObject.GetComponent<AudioSource>();    //Get the Audio source for the cannon
	}
	
	void Update () {
        if (target.activeSelf == false)
            speed = 0;
            FindNewTarget();

        TurnUnit(); //Turn Unit

        if (speed >= maxSpeed)  //Applying speed
            speed = maxSpeed;
        else
            speed += acceleration;

        switch (attackMethod)
        {
            case MethodOfAttack.Spear:
                transform.forward -= new Vector3(.2f, 0f, 0f);
                break;

            case MethodOfAttack.Projectile:
                transform.forward += new Vector3(.1f, 0f, 0f);
                if (gameObject.GetComponent<UnitAttributes>().currentWeapon.name != "Cannon")   //If the current weapon isnt the cannon
                    gameObject.GetComponent<UnitAttributes>().ChangeWeapon();

                Vector3 dis = (target.transform.position - gameObject.transform.position).normalized;

                if (Vector3.Dot(dis, transform.forward) > .8f)    //Who is ahead
                {
                    FireBullet();
                    if (speed > maxSpeed)
                        speed -= acceleration + .1f;
                }
                    
                    break;

            case MethodOfAttack.Mixed:  
                if (gameObject.GetComponent<UnitAttributes>().currentWeapon.name != "Cannon")
                    gameObject.GetComponent<UnitAttributes>().ChangeWeapon();
                break;
        }      
 
        MoveUnit();
	}

    void FireBullet()   //So AI can fire a bullet. Will not go through the Cannon Gameobject
    {
        if(Time.time >= nextfire)
        {
            GameObject temp;
            temp = Instantiate(bullet, bulletspawn.transform.position, new Quaternion()) as GameObject; //Make Bullet
            cannonFire.Play();  //Play Cannon fire Sound
            temp.GetComponent<Bullet_Control>().unitFired = gameObject;                 //Set the unit that fired the bullet
            temp.GetComponent<Bullet_Control>().SetForce(transform.forward * speed);    //Give Bullet force greater than the Unit     

            nextfire = Time.time + firerate;    //Set next fire
        }
        
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
        gameObject.transform.forward = dist.normalized - new Vector3(.2f, 0f, 0f); 
    }

    void FindNewTarget()    //Function to find a new target. Based on closest Unit.
    {
        //Get all Units
        List<GameObject> viableTargets = new List<GameObject>();
        foreach (GameObject u in GameObject.FindGameObjectsWithTag("Unit"))
            if(u != gameObject)
                viableTargets.Add(u);
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            if (p != gameObject)
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