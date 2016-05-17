using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    public Transform Point;     //Where the Empty gameobject is
    public float AreaHit;       //How close the Unit must hit
    bool PointHit = false;
    public GameObject LastRacer;

    public GameObject CheckPosition(GameObject CurrentPoint)
    {
        if ((Point.position.x - AreaHit) <= CurrentPoint.transform.position.x && CurrentPoint.transform.position.x <= (Point.position.x + AreaHit))
        {
            if ((Point.position.z - AreaHit) <= CurrentPoint.transform.position.z && CurrentPoint.transform.position.z <= (Point.position.z + AreaHit))
            {
                PointHit = true;
                return CurrentPoint;
            }
        }
        return null;
    }

    public bool CheckPointHit()
    {
        return PointHit;
    }
}
