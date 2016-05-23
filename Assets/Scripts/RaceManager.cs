using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaceManager : MonoBehaviour
{

    List<GameObject> UnitList;
    GameObject[] UnitWin;
    public int CheckpointAmt;
    List<Checkpoint> Checkpoints;
    public int LapsNeed;
    //public float TimeGameEnd;

    void CheckLap()
    {
        foreach (Checkpoint i in Checkpoints)
        {
            foreach(GameObject j in UnitList)
            {
                UnitAttributes UA = j.GetComponent<UnitAttributes>();
                if (i.CheckPosition(j) &&  UA.nextPoint == i )
                {
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
            foreach (GameObject j in UnitList)
            {
                UnitAttributes A = i.GetComponent<UnitAttributes>();
                UnitAttributes B = i.GetComponent<UnitAttributes>();
                if (i != j) // && i and j havent checked /*ADD integer for points so i can sort*/
                {
                    if (A.lap > B.lap)
                    {
                        A.placeValue;
                    }
                    else if (A.lap < B.lap)
                    {
                        B.placeValue;
                    }
                    else
                    {
                        if(A.checkPoints > B.checkPoints)
                        {
                            A.placeValue;
                        }
                        else if (A.checkPoints < B.checkPoints)
                        {
                            B.placeValue; 
                        }
                        else
                        {
                            float ADis = (i.transform.position - A.nextPoint.transform.position).sqrMagnitude;
                            float BDis = (j.transform.position - B.nextPoint.transform.position).sqrMagnitude;

                            if (ADis > BDis)
                            {
                                A.placeValue;
                            }
                            else if (ADis < BDis)
                            {
                                B.placeValue;
                            }
                        }
                    }
                    //add comparison to a list saying we went through these comparisons
                }
            }
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
        UnitList = new List<GameObject>();
        Checkpoints = new List<Checkpoint>();

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
