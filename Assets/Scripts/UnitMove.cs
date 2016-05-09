using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMove : MonoBehaviour {
    
    //General Unit Info
    float health;
    float speed;
    public float maxSpeed;
    public float acceleration;
    public float handling;
    Transform unitTransform;


	void Start () {
        unitTransform = gameObject.GetComponent<Transform>();
        speed = 0;
	}
	
	void Update () {
        
        if(gameObject.tag == "Player")  //If tyhe Unit is the player
        {
            float rotate = Input.GetAxis("Horizontal");             //Will get Unit's forward
            
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                if(speed >= maxSpeed)
                {
                    unitTransform.Translate(Vector3.forward , Space.Self);
                }

                else
                {
                    unitTransform.Translate(Vector3.forward , Space.Self);
                    speed += acceleration;
                }
            }         

            else   //
            {
                if(speed > 0)
                {
                    speed -= .3f;
                }

                else
                {
                    speed = 0;
                }
            }

            if (rotate != 0 && speed != 0)                        //rotate the Unit.
            {
                gameObject.transform.Rotate(new Vector3(0f, rotate * handling, 0f));
            }
        }

        else
        {

        }
        
	}

    void Lerp(Vector3 speed)    //This function will deal with moving the Unit. Just pass in the vector by how much you would like it to move by.
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + speed.x, gameObject.transform.position.y + speed.y, gameObject.transform.position.z + speed.z);
    }

    void OnCollisionEnter(Collision other)
    {

    }
}