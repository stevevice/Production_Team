using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Utility;

public class RaceManager : MonoBehaviour
{

    List<GameObject> UnitList;
    GameObject[] UnitWin;
    public int CheckpointAmt;
    public List<Checkpoint> Checkpoints;
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

                    if (Checkpoints.IndexOf(UA.nextPoint) + 1 >= Checkpoints.Count)
                    {
                        UA.nextPoint = Checkpoints[0];
                    }

                    else
                    {
                        UA.nextPoint = Checkpoints[Checkpoints.IndexOf(UA.nextPoint) + 1];
                    }


                }
            }
        }
    }

    void CheckPosition()
    {
        List<GameObject> SortUnitList = UnitList;
        
        foreach(GameObject i in SortUnitList)
        {
            foreach (GameObject j in SortUnitList)
            {
                UnitAttributes A = i.GetComponent<UnitAttributes>();
                UnitAttributes B = j.GetComponent<UnitAttributes>();
                if (i != j) // && i and j havent checked /*ADD integer for points so i can sort*/
                {
                    if (A.lap > B.lap)
                    {
                        A.placeValue++;
                    }
                    else if (A.lap < B.lap)
                    {
                        B.placeValue++;
                    }
                    else
                    {
                        if(A.checkPoints > B.checkPoints)
                        {
                            A.placeValue++;
                        }
                        else if (A.checkPoints < B.checkPoints)
                        {
                            B.placeValue++; 
                        }
                        else
                        {
                            float ADis = (i.transform.position - A.nextPoint.transform.position).sqrMagnitude;
                            float BDis = (j.transform.position - B.nextPoint.transform.position).sqrMagnitude;

                            if (ADis > BDis)
                            {
                                A.placeValue++;
                            }
                            else if (ADis < BDis)
                            {
                                B.placeValue++;
                            }
                        }
                    }
                    SortUnitList.Remove(i); //add comparison to a list saying we went through these comparisons
                }
                UnitList.Sort((p1, p2) => A.placeValue.CompareTo(B.placeValue));
            }
        }
        foreach (GameObject i in UnitList)
        {
            UnitAttributes Unit = i.GetComponent<UnitAttributes>();
            Unit.placeValue = 0;
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

	// Use this for initialization
	void Start ()
    {
        UnitList = new List<GameObject>();
        Checkpoints = new List<Checkpoint>();

        foreach(Transform t in GameObject.Find("CheckPoints").GetComponent<WayPointCircuit>().waypointList.items)
        {
            Checkpoints.Add(t.gameObject.GetComponent<Checkpoint>());
        }


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
        CheckPosition();
    }

    
}
