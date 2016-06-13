using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    public List<GameObject> PowerUpList = new List<GameObject>();
    public float TimeTilPUActive;
    public float time;

	// Use this for initialization
	void Start ()
    {
        GameObject[] SpeedPU = GameObject.FindGameObjectsWithTag("SpeedBoostPU");
        GameObject[] HealthPU = GameObject.FindGameObjectsWithTag("HealthBoostPU");

        foreach(GameObject i in SpeedPU)
        {
            PowerUpList.Add(i);
        }
        foreach(GameObject i in HealthPU)
        {
            PowerUpList.Add(i);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        time = Time.time;

	    foreach(GameObject i in PowerUpList)
        {
            PowerUpAction PU = i.GetComponent<PowerUpAction>();

            if(PU.Checked == true && PU.TimeTil <= Time.time)
            {
                i.SetActive(true);
                PU.Checked = false;
            }

            if (i.activeSelf == false && PU.Checked == false)
            {
                PU.TimeTil = Time.time + TimeTilPUActive;
                PU.Checked = true;
            } 
        }
	}
}
