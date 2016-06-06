using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Utility;
using System.Linq;

public class RaceManager : MonoBehaviour
{
    public List<GameObject> UnitList;
<<<<<<< HEAD
    protected List<GameObject> UnitWin;
=======
    List<GameObject> UnitWin;
    protected int CheckpointAmt;
>>>>>>> refs/remotes/atouchetAIE/master
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
                    if (UA.checkPoints == Checkpoints.Count)
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

    class UnitPair
    {
        public GameObject i;
        public GameObject j;

        public UnitPair(GameObject I, GameObject J)
        {
            i = I;
            j = J;
        }
    }

    void CheckPosition()
    {
        List<UnitPair> CheckedList = new List<UnitPair>();
        
        foreach(GameObject i in UnitList)
        {
            foreach (GameObject j in UnitList)
            {
                UnitPair IJ = new UnitPair(i, j);
                UnitPair JI = new UnitPair(j, i);

                if (!CheckedList.Contains(IJ) && !CheckedList.Contains(JI) && i != j)
                {
                    UnitAttributes A = i.GetComponent<UnitAttributes>();
                    UnitAttributes B = j.GetComponent<UnitAttributes>();
                    
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
                            if (A.checkPoints > B.checkPoints)
                            {
                                A.placeValue++;
                            }
                            else if (A.checkPoints < B.checkPoints)
                            {
                                B.placeValue++;
                            }
                            else
                            {
                                float ADis = (i.transform.position - A.nextPoint.transform.position).magnitude;
                                float BDis = (j.transform.position - B.nextPoint.transform.position).magnitude;
                                
                                if (ADis < BDis)
                                {
                                    A.placeValue++;
                                }

                                else
                                {                              
                                    B.placeValue++;
                                }
                                
                            }
                        }
                        CheckedList.Add(IJ);//add comparison to a list saying we went through these comparisons
                    
                }
            }
        }


        UnitList = UnitList.OrderByDescending(x=>x.GetComponent<UnitAttributes>().placeValue).ToList();



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
<<<<<<< HEAD
            if (i.GetComponent<UnitAttributes>().lap >= LapsNeed)
            {
                UnitWin.Add(i);
            }
=======
            //if (i.GetComponent<UnitAttributes>().lap >= LapsNeed)
            //{
            //    UnitWin[AmtPlayer] = i;
            //} 
>>>>>>> refs/remotes/atouchetAIE/master
        }

        foreach(GameObject i in UnitWin)
        {
            if(i.gameObject.tag.ToString() == "Player")
            {

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
        UnitWin = new List<GameObject>();
        Checkpoints = new List<Checkpoint>();

        foreach(Transform t in GameObject.Find("CheckPoints").GetComponent<WaypointCircuit>().waypointList.items)
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

        foreach(GameObject i in UnitList)
        {
            i.GetComponent<UnitAttributes>().nextPoint = Checkpoints [0];
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
