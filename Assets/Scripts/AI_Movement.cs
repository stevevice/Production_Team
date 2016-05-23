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
    [SerializeField] private Behavior unitBehavior;
    Rigidbody unitRB;

    WaypointProgressTracker proTracker;
    Transform target;                   //Target for the Unit.
    Transform unitTransform;
    Vector3 fwd;

    void Start () {
        unitRB = gameObject.GetComponent<Rigidbody>();
        unitTransform = gameObject.transform;
        proTracker = gameObject.GetComponent<WaypointProgressTracker>();
        target = proTracker.target;
        fwd = transform.forward / 100;
	}
	
	void Update () {
        Vector3 dist = target.transform.position - gameObject.transform.position;
        gameObject.transform.forward = dist.normalized;

        switch (unitBehavior)
        {
            case Behavior.Regular:

                break;

            case Behavior.Cautious:

                break;

            case Behavior.Reckless:

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
