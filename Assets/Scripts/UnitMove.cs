using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMove : MonoBehaviour {
    
    //General Unit Info
    float health;               //How far away from being destroyed
    float speed;                //How fast is the Unit moving
    public float maxSpeed;      //What is the fastes possible speed for a Unit to move at
    public float acceleration;  //How much speed is added when able to accelerate.
    public float handling;      //How fast can the Unit turn?
    Transform unitTransform;    //The Transform of the Unit.

    Vector3 forward;    //This Vector will referance the forward of an object.

    //AI Controlled Unit Variables
    public List<Transform> points;
    Transform goToPoint;
    NavMeshAgent nav;

    void Start () {
        unitTransform = gameObject.GetComponent<Transform>();   //Get transform
        speed = 0;                                              //Set speed
        forward = Vector3.forward / 200;                        //Scaling the forward. Vector3.forward was too much by itself
        goToPoint = points[0];
        if (!gameObject.CompareTag("Player")){
            nav = gameObject.GetComponent<NavMeshAgent>();
            nav.SetDestination(goToPoint.position);
            nav.angularSpeed = handling;
        }       
    }
	
	void FixedUpdate () {
        
        if(gameObject.tag == "Player")  //If tyhe Unit is the player
        {
            float rotate = Input.GetAxis("Horizontal");             //Will get Unit's forward
            
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))    //If player pushes down the "gas"
            {
                if(speed < maxSpeed)    //If the Unit Isn't going at max speed
                {
                    speed += acceleration;  //add speed
                }         
            }         

            else    //else if no input
            {
                if(speed > 0)   //speed is greater than 0
                {
                    speed -= .3f;   //decreace speed
                }

                else      //else set speed to 0.
                {
                    speed = 0;
                }
            }

            if (rotate != 0 && speed != 0)      //if we have input and we are moving
            {
                gameObject.transform.Rotate(new Vector3(0f, rotate * handling, 0f));    //Rotate
            }

            MoveUnit();
            
        }

        else     //Else if not the player
        {
            if (speed < maxSpeed)
            {
                speed += acceleration;
            }

            else
            {
                speed = maxSpeed;
            }

            MoveUnit();

            float dist = Vector3.Distance(gameObject.transform.position, goToPoint.position);
            if (nav.remainingDistance != Mathf.Infinity && dist <= nav.stoppingDistance)
            {
                if (points.IndexOf(goToPoint) + 1 >= points.Count)
                {
                    goToPoint = points[0];
                    nav.destination = goToPoint.position;
                }

                else
                {
                    goToPoint = points[points.IndexOf(goToPoint) + 1];
                    nav.destination = goToPoint.position;
                }
            }           
        }
	}

    void MoveUnit() //Locgic that determines how fast a Unit goes
    {
        if (speed >= maxSpeed)  //If going faster than max speed
        {
            unitTransform.Translate(forward * maxSpeed, Space.Self);    //move at max speed
        }

        else       //else if not going at max speed
        {
            unitTransform.Translate(forward * speed, Space.Self);   //move at speed
        }
    }

    void OnCollisionEnter(Collision other)  //If collides with another collider
    {

    }
}