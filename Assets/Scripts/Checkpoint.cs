using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour
{
    public float AreaHit;       //How close the Unit must hit
    public List<GameObject> Racers; //list that will not be edited in this script but in the RaceManager

    static public bool CheckPosition(GameObject Unit, float AreaHit, GameObject CheckP)
    {
        if ((CheckP.transform.position.x - AreaHit) <= Unit.transform.position.x && Unit.transform.position.x <= (CheckP.transform.position.x + AreaHit))
        {
            if ((CheckP.transform.position.z - AreaHit) <= Unit.transform.position.z && Unit.transform.position.z <= (CheckP.transform.position.z + AreaHit))
            {
                return true;
            }
        }
        return false;
    }

    void Start()
    {
        Racers = new List<GameObject>();
    }
}
