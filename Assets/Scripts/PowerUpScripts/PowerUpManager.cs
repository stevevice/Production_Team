using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    public List<GameObject> PowerUpList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        GameObject[] SpeedPU = GameObject.FindGameObjectsWithTag("SpeedBoostPU");
        GameObject[] HealthPU = GameObject.FindGameObjectsWithTag("HealthBoostPU");

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
