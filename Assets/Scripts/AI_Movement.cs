using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Utility;

public class AI_Movement : MonoBehaviour {

    enum Braking
    {

    }

    bool driving;
    public float maxSpeed;                   //The speed the Unit can not go past
    [SerializeField] private float speed;    //Current speed of the Unit

    WaypointProgressTracker proTracker;
    Transform target;                   //Target for the Unit.
    Transform unitTransform;
    Vector3 forward;

    void Start () {
        unitTransform = gameObject.transform;
        proTracker = gameObject.GetComponent<WaypointProgressTracker>();
        target = proTracker.target;
       
	}
	
	void Update () {
        Vector3 dist = target.transform.position - gameObject.transform.position;
        forward = dist.normalized / 50;
        MoveUnit();
    }

    void MoveUnit()
    {
        if (speed >= maxSpeed)  //If going faster than max speed
        {
            unitTransform.Translate(forward * maxSpeed, Space.Self);    //move at max speed
        }

        else       //else if not going at max speed
        {
            unitTransform.Translate(forward * speed, Space.Self);   //move at speed
        }
    }
}
