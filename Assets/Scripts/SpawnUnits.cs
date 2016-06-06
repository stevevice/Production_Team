using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Utility;

public class SpawnUnits : MonoBehaviour {

    public Transform playerSpawn;
    public List<Transform> unitSpawns;

    public List<GameObject> unitPrefabs;

	void Awake () {
	    //Load in Player

        foreach(Transform sp in unitSpawns)
        {
            LoadUnit(sp);
        }
	}
	


    void LoadPlayer()
    {

    }

    void LoadUnit(Transform pos)
    {
        int val = Random.Range(0, unitPrefabs.Count - 1);
        GameObject unit;
        unit = Instantiate(unitPrefabs[val], pos.position, new Quaternion()) as GameObject;
        unit.GetComponent<WaypointProgressTracker>().circuit = GameObject.Find("CheckPoints").GetComponent<WaypointCircuit>();
        unit.transform.LookAt(unit.GetComponent<WaypointProgressTracker>().circuit.waypointList.items[0]);
    }
}
