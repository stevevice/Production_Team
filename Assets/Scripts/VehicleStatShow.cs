﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VehicleStatShow : MonoBehaviour {

    public List<GameObject> buttonList;
    public List<Canvas> stats;

	void Start () {
        foreach(Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

        foreach (Canvas c in stats)
        {
            c.gameObject.SetActive(false);
        }
    }

	// Update is called once per frame
	void Update () {

        PointerEventData cursor = new PointerEventData(EventSystem.current); //Get the active cursor in the game                          // This section prepares a list for all objects hit with the raycast
        cursor.position = Input.mousePosition; //Set the cursur positon equal to the mouses position
        List<RaycastResult> objectsHit = new List<RaycastResult>(); //Creates a list of all objects hit by the raycast
        EventSystem.current.RaycastAll(cursor, objectsHit); //Performs the raycst from the cursor position and returns all objects hit and places them in the objectsHit list

        if(objectsHit.Count > 0) //If objectHit has elements in it
        {
            if(buttonList.Contains(objectsHit[0].gameObject.transform.parent.gameObject))//Check if the object is part of a car button
            {
                foreach(Transform tran in transform)
                {
                    if (tran.gameObject.name == objectsHit[0].gameObject.transform.parent.gameObject.name)
                    {
                        tran.gameObject.SetActive(true);
                        Player_Move unitMove = tran.gameObject.GetComponent<Player_Move>();

                        GameObject canvas = null;
                        foreach(Canvas c in stats)
                        {
                            c.gameObject.SetActive(false);
                            if(c.gameObject.name == tran.gameObject.name)
                            {
                                canvas = c.gameObject;
                                canvas.SetActive(true);
                            }
                        }

                        //Set Stats
                        foreach(Transform child in canvas.transform)
                        {
                            switch (child.gameObject.name)
                            {
                                case "TopSpeed":
                                    child.GetComponentInChildren<Slider>().value = unitMove.maxSpeed;
                                    break;

                                case "Handling":
                                    child.GetComponentInChildren<Slider>().value = unitMove.handling;
                                    break;

                                case "Acceleration":
                                    child.GetComponentInChildren<Slider>().value = unitMove.acceleration;
                                    break;

                                default:
                                    break;
                            }
                        }
                        
                        tran.gameObject.transform.position = tran.gameObject.GetComponent<KeepInBounds>().origin;
                        unitMove.speed = 0;
                        tran.rotation = new Quaternion(0, 0, 0, 0);
                    }

                    else
                        tran.gameObject.SetActive(false);                    
                }
                
            }
        }
    }
}
