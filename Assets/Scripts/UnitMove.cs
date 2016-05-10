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
    public float closness; 
    public List<Transform> points;
    [SerializeField]
    Transform goToPoint;
    Vector3 min;
    Vector3 max;

    void Start () {
        unitTransform = gameObject.GetComponent<Transform>();   //Get transform
        speed = 0;                                              //Set speed
        forward = Vector3.forward / 200;                        //Scaling the forward. Vector3.forward was too much by itself
        goToPoint = points[0];
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
            if (speed < maxSpeed)    //If the Unit Isn't going at max speed
            {
                speed += acceleration;  //add speed
            }

            Vector3 dist = (goToPoint.position - gameObject.transform.position).normalized;
            float dotProd = Vector3.Dot(dist, gameObject.transform.forward);

            if(dotProd < .999f)
            {
                gameObject.transform.Rotate(new Vector3(0f, handling, 0f));    //Rotate
            }

            min = goToPoint.transform.position - new Vector3(closness, 0, closness);
            max = goToPoint.transform.position + new Vector3(closness, 0, closness);

            if(IsClosToTarget(min, max))
            {
                
                if(points.IndexOf(goToPoint) + 1 == points.Count)
                {
                    goToPoint = points[0];
                }

                else
                {
                    goToPoint = points[points.IndexOf(goToPoint) + 1];
                }
            }

            MoveUnit();
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

    bool IsClosToTarget(Vector3 min, Vector3 max)
    {
        if(min.x <= gameObject.transform.position.x && gameObject.transform.position.x <= max.x)
        {
            if (min.z <= gameObject.transform.position.z && gameObject.transform.position.z <= max.z)
            {
                Debug.Log("Close");
                return true;
            }
        }

        return false;
    }

    void OnCollisionEnter(Collision other)  //If collides with another collider
    {

    }
}