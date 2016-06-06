using UnityEngine;
using System.Collections;

public class PowerUpAttributes : MonoBehaviour
{
    public bool SpeedBoostPU = false;
    bool SpeedBoostActive = false;
    public bool HealthIncPU = false;
    public float HealthInc = 25;
    float OldSpeed;
    public float NewSpeed = 75;
    float OldAcceleration;
    float NewAcceleration = 1f;
    public float TimeLeft = 10.0f;

    void SpeedBoost()
    {
        if(gameObject.CompareTag("Player"))
        {
            Player_Move Racer = gameObject.GetComponent<Player_Move>();
            Racer.maxSpeed = ((NewSpeed * .01f) * OldSpeed) + OldSpeed;
            Racer.acceleration = NewAcceleration;
            SpeedBoostActive = true;
        }
        else if(gameObject.CompareTag("Unit"))
        {
            AI_Movement Racer = gameObject.GetComponent<AI_Movement>();
            Racer.maxSpeed = ((NewSpeed * .01f) * OldSpeed) + OldSpeed;
            Racer.acceleration = NewAcceleration;
            SpeedBoostActive = true;
        }
        SpeedBoostPU = false;
    }

    void SpeedReset()
    {
        if (gameObject.CompareTag("Player"))
        {
            Player_Move Racer = gameObject.GetComponent<Player_Move>();
            Racer.maxSpeed = OldSpeed;
            Racer.acceleration = OldAcceleration;
            SpeedBoostActive = false;
        }
        else if (gameObject.CompareTag("Unit"))
        {
            AI_Movement Racer = gameObject.GetComponent<AI_Movement>();
            Racer.maxSpeed = OldSpeed;
            Racer.acceleration = OldAcceleration;
            SpeedBoostActive = false;
        }
        TimeLeft = 10.0f;
    }

    void HealthBoost()
    {
        UnitAttributes Racer = gameObject.GetComponent<UnitAttributes>();
        Racer.health = Racer.health + HealthInc;
        HealthIncPU = false;
    }

	// Use this for initialization
	void Start ()
    {
        if (gameObject.CompareTag("Player"))
        {
            Player_Move Racer = gameObject.GetComponent<Player_Move>();
            OldSpeed = Racer.maxSpeed;
            OldAcceleration = Racer.acceleration;
        }
        else if (gameObject.CompareTag("Unit"))
        {
            AI_Movement Racer = gameObject.GetComponent<AI_Movement>();
            OldSpeed = Racer.maxSpeed;
            OldAcceleration = Racer.acceleration;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Q) && SpeedBoostPU == true)
        {
            SpeedBoost();
        }
        else if (Input.GetKeyUp(KeyCode.Q) && HealthIncPU == true)
        {
            HealthBoost();
        }

        if(SpeedBoostActive)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft <= 0)  
            {
                SpeedReset();
            } 

        }
    }
}
