using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Utility;

public class AI_Movement : MonoBehaviour {

    enum Behavior
    {
        Cautious = 0,
        Reckless = 1,
        Regular = 2,
    }
        
    public float maxSpeed;                          //The speed the Unit can not go past
    public float speed;                             //Current speed of the Unit
    [SerializeField] public float acceleration;    //How much speed is added when able to accelerate.
    private float handling;                         //How fast can the Unit turn?
    [SerializeField] private Behavior unitBehavior; //How will this Unit act

    WaypointProgressTracker proTracker;         //To get the tracker that follows the track
    [HideInInspector] public Transform target;  //Target for the Unit.
    Transform unitTransform;                    //Transform of the Unit
    Vector3 fwd;                                //Variable for Pushing the Object towards its Z axis
    Vector3 dist;                               //Distance Between self and target.

    //Behavior Variables
    [HideInInspector] public List<GameObject> otherUnits;   //All the other Units. SELF IS NOT INCLUDED IN THIS
    public float avoidanceDist;

    void Start () {
        //Populate all the Other Units
        foreach(UnitAttributes u in GameObject.FindObjectsOfType(typeof(UnitAttributes)))
        {
            if(u.gameObject != gameObject)
            {
                otherUnits.Add(u.gameObject);
            }
        }

        unitTransform = gameObject.transform;   //Set transform
        proTracker = gameObject.GetComponent<WaypointProgressTracker>();    //Get Waypoint tracker
        target = proTracker.target;         //Set the Target
        fwd = transform.forward / 100;  //Scale the Foreward

        switch (unitBehavior)
        {
            case Behavior.Regular:  //Regular Behavior Speed Set
                maxSpeed = maxSpeed * .9f;
                break;

            case Behavior.Cautious: //Cautious Behavior Speed Set
                maxSpeed = maxSpeed * .75f;    
                break;

            case Behavior.Reckless: //Reckless Behavior Speed Set
                break;
        }
	}
	
	void Update () {
        if (!GoAroundUnits())
        {
            dist = target.transform.position - gameObject.transform.position;
            gameObject.transform.forward = dist.normalized;
        }


        switch (unitBehavior)   //Switch for the Behaviors of the Units
        {
            case Behavior.Regular:  //If the Unit is a regular
                if (!AvoidOthers() && !TakeTurnsSlow()) //if not takiung a turn slow or avoiding others
                {
                    if (speed < maxSpeed)    //If the Unit Isn't going at max speed
                    {
                        speed += acceleration;  //add speed
                    }

                    else
                    {
                        speed = maxSpeed; //speed is equal to little less than max
                    }
                }

                //If Unit is taking Turn slow or avoiding others, decrease speed
                else
                    if (speed > maxSpeed * .25)
                        speed -= acceleration + .1f;
                break;

            case Behavior.Cautious: //Cautious Behavior
                if (!AvoidOthers() && !TakeTurnsSlow()) //Avoid others and take turns slow
                {
                    if (speed < maxSpeed)    //If the Unit Isn't going at max speed
                    {
                        speed += acceleration;  //add speed
                    }

                    else
                    {
                        speed = maxSpeed;
                    }
                }
                //If taking turn slow or avoiding others, decrease speed
                else
                    if (speed > maxSpeed * .25)
                        speed -= acceleration + .1f;
                break;

            case Behavior.Reckless: //Reckless Behavior
                if (!TakeTurnsSlow())   //Only takes Turns slow
                {
                    if (speed < maxSpeed)    //If the Unit Isn't going at max speed
                    {
                        speed += acceleration;  //add speed
                    }

                    else
                    {
                        speed = maxSpeed;
                    }
                }
                //If taking turn slow, decreace speed
                else
                    if (speed > maxSpeed * .25)
                        speed -= acceleration + .1f;
                break;
        }

        
        MoveUnit(); //Moving the Unit is Universal
    }

    void MoveUnit()
    {
        if (speed >= maxSpeed)  //If going faster than max speed
        {
            unitTransform.Translate(fwd * maxSpeed, Space.Self);    //move at max speed
        }

        else       //else if not going at max speed
        {
            unitTransform.Translate(fwd * speed, Space.Self);   //move at speed
        }
    }

    bool AvoidOthers()  //Function that will decrease speed if the unit is about to crash with another
    {
        foreach(GameObject u in otherUnits) //Remove all the Units that are turned off
        {
            if(u.activeSelf == false)
            {
                otherUnits.Remove(u);
            }
        }

        //Find closest Unit
        Transform closeUnit = null;
        Vector3 minDis = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        Vector3 dist = new Vector3();

        foreach (GameObject u in otherUnits)
        {
            dist = u.transform.position - gameObject.transform.position;
            if(dist.sqrMagnitude < minDis.sqrMagnitude)
            {
                minDis = dist;
                closeUnit = u.transform;
            }
        }

        //Compare the closest Unit to Slef
        if(closeUnit != null && (closeUnit.position - gameObject.transform.position).magnitude <= avoidanceDist)
        {
            Vector3 dis = (closeUnit.position - gameObject.transform.position).normalized;

            if (Vector3.Dot(dis, unitTransform.forward) > 0 && speed > 0)    //Who is ahead
                return true;  
        }
        return false;
    }

    bool TakeTurnsSlow()    //Take Turnning Slowly
    {
        if (Vector3.Dot(target.forward, unitTransform.forward) > -.5f && Vector3.Dot(target.forward, unitTransform.forward) < .5f)  //Target is the point the Unit is trying to go to. Target does rotate to predict path
        {
            return true;
        }

        return false;
    }

    bool GoAroundUnits()
    {
        foreach (GameObject u in otherUnits) //Remove all the Units that are turned off
        {
            if (u.activeSelf == false)
            {
                otherUnits.Remove(u);
            }
        }

        //Find closest Unit
        Transform closeUnit = null;
        Vector3 minDis = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        Vector3 dist = new Vector3();

        foreach (GameObject u in otherUnits)
        {
            dist = u.transform.position - gameObject.transform.position;
            if (dist.sqrMagnitude < minDis.sqrMagnitude)
            {
                minDis = dist;
                closeUnit = u.transform;
            }
        }

        if (closeUnit == null)
            return false;

        AI_Movement closeUnitAI;

        closeUnitAI = closeUnit.gameObject.GetComponent<AI_Movement>();


        if (closeUnitAI.maxSpeed < maxSpeed && closeUnitAI.speed <= speed - 2 && minDis.magnitude <= avoidanceDist)
        {
            if (Vector3.Dot(minDis, unitTransform.forward) > -1f && minDis.magnitude <= avoidanceDist)    //Who is ahead
            {
                transform.forward = closeUnitAI.gameObject.transform.forward - closeUnitAI.gameObject.transform.right;
                return true;
            }
        }
        return false;
    }
}
