﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityStandardAssets.Utility;
using System.Linq;

public class RaceManager : MonoBehaviour
{
    public List<GameObject> UnitList;
    List<GameObject> UnitWin;

    protected int CheckpointAmt;
    public List<Checkpoint> Checkpoints;
    public int LapsNeed;
    //public float TimeGameEnd;
    public UnitAttributes player;
    public GameObject endCamera;

    void CheckLap()
    {
        foreach (Checkpoint i in Checkpoints)
        {
            foreach (GameObject j in UnitList)
            {
                UnitAttributes UA = j.GetComponent<UnitAttributes>();
                if (i.CheckPosition(j) && UA.nextPoint == i)
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

        foreach (GameObject i in UnitList)
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


        UnitList = UnitList.OrderByDescending(x => x.GetComponent<UnitAttributes>().placeValue).ToList();



        foreach (GameObject i in UnitList)
        {
            UnitAttributes Unit = i.GetComponent<UnitAttributes>();
            Unit.placeValue = 0;
        }
    }

    void CheckGoal()
    {
        if ((player.lap >= LapsNeed || UnitList.Count == 1) && endCamera.activeSelf != true)
        {
            GameObject UI = GameObject.Find("UI");
            foreach (Transform go in UI.transform)
            {
                if (go.gameObject.name != "BackButton" && go.gameObject.name != "SceneSelection" && go.gameObject.name != "EventSystem")
                    go.gameObject.SetActive(false);
            }

            player.gameObject.GetComponent<WaypointProgressTracker>().enabled = true;
            player.gameObject.GetComponent<AI_Movement>().enabled = true;
            player.gameObject.transform.position = new Vector3(Checkpoints[0].transform.position.x, player.gameObject.transform.position.y, Checkpoints[0].transform.position.z);
            player.gameObject.GetComponent<AI_Movement>().speed = player.gameObject.GetComponent<Player_Move>().speed;
            player.gameObject.GetComponent<Player_Move>().enabled = false;

            GameObject.Find("WaypointParticles").SetActive(false);

            player.gameObject.transform.forward = new Vector3(0f, 0f, 1f);

            player.gameObject.transform.FindChild("Camera").gameObject.SetActive(false);
            endCamera.SetActive(true);
        }
    }

    void CheckPlayersAlive()
    {
        Utilities.RemoveAt(UnitList, delegate (GameObject go)
        {
            if (go.GetComponent<UnitAttributes>().health <= 0)
                return true;
            return false;
        });
    }

    // Use this for initialization
    void Start()
    {
        if (endCamera.activeSelf == true)
            endCamera.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitAttributes>();
        UnitList = new List<GameObject>();
        Checkpoints = new List<Checkpoint>();
        CheckpointAmt = 0;

        foreach (Transform t in GameObject.Find("CheckPoints").GetComponent<WaypointCircuit>().waypointList.items)
        {
            Checkpoints.Add(t.gameObject.GetComponent<Checkpoint>());
            CheckpointAmt++;
        }


        GameObject[] Units = GameObject.FindGameObjectsWithTag("Unit");
        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject i in Units)
        {
            UnitList.Add(i);
            i.GetComponent<AI_Movement>().enabled = false;
        }
        foreach (GameObject j in Player)
        {
            UnitList.Add(j);
            j.GetComponent<Player_Move>().enabled = false;
        }

        foreach (GameObject i in UnitList)
        {
            i.GetComponent<UnitAttributes>().nextPoint = Checkpoints[0];
            i.transform.LookAt(new Vector3(Checkpoints[0].transform.position.x, i.transform.position.y, Checkpoints[0].transform.position.z));
        }

        Utilities.Wait(3, this, StartRace);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayersAlive();
        CheckLap();
        CheckGoal();
        CheckPosition();
    }

    void StartRace()
    {
        foreach (GameObject go in UnitList)
        {
            if (go.CompareTag("Player"))
            {
                go.GetComponent<Player_Move>().enabled = true;
            }

            else if (go.CompareTag("Unit"))
            {
                go.GetComponent<AI_Movement>().enabled = true;
            }
        }
    }
}