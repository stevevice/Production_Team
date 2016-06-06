using UnityEngine;
using System.Collections;

public class PowerUpAttributes : MonoBehaviour
{
    public bool SpeedBoostPU = false;
    public bool HealthIncPU = false;
    float HealthInc = 25;
    float Speed = 75;

    void SpeedBoost()
    {
        if(gameObject.CompareTag("Player"))
        {
            Player_Move Racer = new Player_Move();
            Racer.maxSpeed = Speed;
            Racer.acceleration = .5f;
        }
        else
        {
            AI_Movement Racer = new AI_Movement();
        }
    }

    void HealthBoost()
    {
        UnitAttributes Racer = new UnitAttributes();
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Q) && SpeedBoostPU == true)
        {

        }
        else if (Input.GetKeyUp(KeyCode.Q) && HealthIncPU == true)
        {

        }
        

    }
}
