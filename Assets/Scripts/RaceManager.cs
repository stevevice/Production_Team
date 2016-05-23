using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaceManager : MonoBehaviour
{

    List<GameObject> UnitList = new List<GameObject>();
    GameObject[] UnitWin;
    public int CheckpointAmt;
    //public int NumCheckpoints;
    //public Checkpoint[NumCheckpoints] CPArray = new Checkpoint[];
    List<Checkpoint> Checkpoints = new List<Checkpoint>();
    public int LapsNeed;
    int Laps = 1;
    public float TimeGameEnd;

    void CheckLap()
    {
        foreach (Checkpoint i in Checkpoints)
        {
            foreach(GameObject j in UnitList)
            {
                if (i.CheckPosition(j) == true)
                {
                    j.GetComponent<UnitAttributes>().checkPoints++;
                    if (j.GetComponent<UnitAttributes>().checkPoints == CheckpointAmt)
                    {
                        j.GetComponent<UnitAttributes>().lap++;
                        j.GetComponent<UnitAttributes>().checkPoints = 0;
                    }
                }
            }
        }
    }

    void CheckGoal()
    {
        foreach(GameObject i in UnitList)
        {
            int AmtPlayer = 0;
            AmtPlayer++;

            if (i.GetComponent<UnitAttributes>().lap >= LapsNeed)
            {
                UnitWin[AmtPlayer] = i;
            } 
        }
    }

    void CheckPlayersAlive()
    {
        foreach(GameObject i in UnitList)
        {
            if(i.GetComponent<UnitAttributes>().health == 0)
            {
                UnitList.Remove(i);
            }
        }
    }

	// Use this for initialization
	void Start ()
    {
        GameObject[] Units = GameObject.FindGameObjectsWithTag("Unit");
        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject i in Units)
        {
            UnitList.Add(i);
        }
        foreach (GameObject j in Player)
        {
            UnitList.Add(j);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckLap();
        CheckGoal();
        CheckPlayersAlive();
    }

    
}
