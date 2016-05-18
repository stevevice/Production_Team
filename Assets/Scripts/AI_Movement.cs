using UnityEngine;
using System.Collections.Generic;

public class AI_Movement : MonoBehaviour {

    enum Braking
    {

    }

    bool driving;
    public float maxSpeed;                   //The speed the Unit can not go past
    [SerializeField] private float speed;    //Current speed of the Unit

    public List<Transform> wayPoints;   //Waypoints for the Unit
    Transform target;                   //Target for the Unit.

	void Start () {
	    for(int i = 0; i < wayPoints.Count - 1; i++)
        {
            NavMesh.CalculatePath();
        }
	}
	
	void Update () {
	
	}

    public void SetTar(Transform tar)
    {
        target = tar;
        driving = true;
    }
}
