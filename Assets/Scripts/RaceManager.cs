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
    public float TimeGameEnd;

    void CheckLap()
    {
        foreach (Checkpoint i in Checkpoints)
        {
            foreach(GameObject j in UnitList)
            {
                if (i.CheckPosition(j) /*&&  j.nextPoint == i */)
                {
                    UnitAttributes UA = j.GetComponent<UnitAttributes>();
                    UA.checkPoints++;
                    if (UA.checkPoints == CheckpointAmt)
                    {
                        UA.lap++;
                        UA.checkPoints = 0;
                    }
                }
            }
        }
    }

    void CheckPosition()
    {
        List<GameObject> SortUnitList = UnitList;
        
        foreach(GameObject i in UnitList)
        {
            if ()

                playerList.Sort((p1, p2) => p1.score.CompareTo(p2.score));
        }
    }

    void CheckGoal()
    {
        foreach(GameObject i in UnitList)
        {
            int AmtPlayer = 1;

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
            if(i.GetComponent<UnitAttributes>().health <= 0)
            {
                UnitList.Remove(i);
            }
        }
    }

    void CheckPlacement()
    {

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
