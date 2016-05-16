using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMove : MonoBehaviour {
    
    //General Unit Info
    public float speed;         //How fast is the Unit moving
    public float maxSpeed;      //What is the fastes possible speed for a Unit to move at
    public float acceleration;  //How much speed is added when able to accelerate.
    public float handling;      //How fast can the Unit turn?
    Transform unitTransform;    //The Transform of the Unit.
    float rotate;

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
        nav = gameObject.GetComponent<NavMeshAgent>();      //Set Nav Agent
        if (!gameObject.CompareTag("Player")){              //If not the player
            nav.SetDestination(goToPoint.position);         //set Destination
            nav.angularSpeed = handling;            //set angular speed to handling son we do not have to touch the Nav Agent
        }
         
        else       //If the player 
        {
            points = null;  //Points are empty
        }
    }
	
	void FixedUpdate () {
        
        if(gameObject.tag == "Player")  //If tyhe Unit is the player
        {
            if(speed > 0)
                rotate = Input.GetAxis("Horizontal");             //Will get Unit's forward     
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))    //If player pushes down the "gas"
            {               
                if (speed < maxSpeed)    //If the Unit Isn't going at max speed
                {
                    speed += acceleration;  //add speed
                }         
            }         

            else    //else if no gas
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
                if(speed / maxSpeed < .5f)  //Prevents player from spinning out at low speeds
                    gameObject.transform.Rotate(new Vector3(0f, rotate * (handling + (handling * .5f))  / 50, 0f));    //Rotate
                else
                    gameObject.transform.Rotate(new Vector3(0f, rotate * (handling / (speed / maxSpeed)) / 50, 0f));    //Rotate    
            }

            MoveUnit();     //Move the Unit
            
        }

        else if (!gameObject.CompareTag("Player"))    //Else if not the player
        {
            
            if (speed < maxSpeed)       //If we are not going at max speed
            {
                speed += acceleration;  //Increace speed
            }

            else       //If speed is greater than max
            {
                speed = maxSpeed;   //speed is max
            }

            if(speed > 0)   //Handiling control for the AI
            {
                nav.angularSpeed = handling / (speed / maxSpeed);
                if(nav.angularSpeed > handling * .5f)
                {
                    nav.angularSpeed = handling * .5f;
                }
            }
                

            MoveUnit();     //Move Unit

            if (nav.currentOffMeshLinkData.activated)   //if Link hit, give speed to navigate
            {
                nav.speed = speed * .125f;
            }

            else     //Else return normal speed
            {
                nav.speed = .1f;
            }

            float dist = Vector3.Distance(gameObject.transform.position, goToPoint.position);   //How far away are we from the destination
            if (nav.remainingDistance != Mathf.Infinity && dist <= nav.stoppingDistance)        //If within bounds of the acceptable area
            {
                if (points.IndexOf(goToPoint) + 1 >= points.Count)  //if the last item in the list
                {
                    goToPoint = points[0];                  //Next point is the beginning
                    nav.destination = goToPoint.position;   //switch destination
                }

                else
                {
                    goToPoint = points[points.IndexOf(goToPoint) + 1];  //Set next point
                    nav.destination = goToPoint.position;               //Go to the next point
                }
            }           
        }
	}

    void MoveUnit() //Locgic that determines how fast a Unit goes
    {
        //Debug.Log(nav.velocity + "Dot Product: " + nav.velocity.magnitude);       ///Key to changing velocity
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