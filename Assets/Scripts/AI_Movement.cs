using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Utility;

public class AI_Movement : MonoBehaviour {

    enum Behavior
    {
        Cautious,
        Reckless,
        Regular,
    }

    public float maxSpeed;                   //The speed the Unit can not go past
    [SerializeField] private float speed;    //Current speed of the Unit
    [SerializeField] private float acceleration;  //How much speed is added when able to accelerate.
    [SerializeField] private float handling;      //How fast can the Unit turn?
    [SerializeField] private Behavior unitBehavior;

    WayPointProgressTracker proTracker;
    Transform target;                   //Target for the Unit.
    Transform unitTransform;
    Vector3 fwd;                        //Variable for Pushing the Object towards its Z axis
    Vector3 dist;

    void Start () {
        unitTransform = gameObject.transform;
        proTracker = gameObject.GetComponent<WayPointProgressTracker>();
        target = proTracker.target;
        fwd = transform.forward / 100;
	}
	
	void Update () {
        
        switch (unitBehavior)
        {
            case Behavior.Regular:
                dist = target.transform.position - gameObject.transform.position;
                gameObject.transform.forward = dist.normalized * .9f;

                if (speed < maxSpeed * .9f)    //If the Unit Isn't going at max speed
                {
                    speed += acceleration;  //add speed
                }

                else
                {
                    speed = maxSpeed * .9f;
                }

                break;

            case Behavior.Cautious:
                dist = target.transform.position - gameObject.transform.position;
                gameObject.transform.forward = dist.normalized;
                break;

            case Behavior.Reckless:
                dist = target.transform.position - gameObject.transform.position;
                gameObject.transform.forward = dist.normalized * .8f;
                break;
        }

        MoveUnit();
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
}
