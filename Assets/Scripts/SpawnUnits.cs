using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityStandardAssets.Utility;


public class SpawnUnits : MonoBehaviour {

    public Transform playerSpawn;
    public List<Transform> unitSpawns;

    public List<GameObject> unitPrefabs;
    public List<GameObject> playerPrefabs;

	void Awake () {
        LoadPlayer();

        foreach(Transform sp in unitSpawns)
        {
            LoadUnit(sp);
        }
	}
	


    void LoadPlayer()
    {
        StreamReader file = new StreamReader(Environment.CurrentDirectory + "CurrentVehicle.txt");
        string val = file.ReadLine();

        foreach(GameObject go in playerPrefabs)
        {
            if(go.name == val)
            {
                GameObject unit;
                unit = Instantiate(go, playerSpawn.position, new Quaternion()) as GameObject;
                unit.GetComponent<CheckPointHighlight>().particles = GameObject.Find("WaypointParticles");
                unit.GetComponent<CheckPointHighlight>().circet = GameObject.Find("CheckPoints");
                unit.GetComponent<WaypointProgressTracker>().circuit = GameObject.Find("CheckPoints").GetComponent<WaypointCircuit>();
                GameObject.Find("UI").GetComponent<HealthBar>().Unit = unit.GetComponent<UnitAttributes>();
                unit.GetComponent<WaypointProgressTracker>().circuit = GameObject.Find("CheckPoints").GetComponent<WaypointCircuit>();
                unit.transform.LookAt(unit.GetComponent<WaypointProgressTracker>().circuit.waypointList.items[0]);
            }
        }

    }

    void LoadUnit(Transform pos)
    {
        int val = UnityEngine.Random.Range(0, unitPrefabs.Count - 1);
        GameObject unit;
        unit = Instantiate(unitPrefabs[val], pos.position, new Quaternion()) as GameObject;
        unit.GetComponent<WaypointProgressTracker>().circuit = GameObject.Find("CheckPoints").GetComponent<WaypointCircuit>();
    }
}
