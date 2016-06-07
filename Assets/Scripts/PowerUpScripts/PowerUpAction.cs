using UnityEngine;
using System.Collections;

public class PowerUpAction : MonoBehaviour
{
    bool GoDown = false;
    public float speed = .25f;
    Vector3 HovCurrent;
    float HovMin;
    float HovMax;
    public float TimeTil;
    public bool Checked = false;

    void Start()
    {
        HovCurrent = gameObject.transform.position;
        HovMin = HovCurrent.y;
        HovMax = HovCurrent.y + 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y >= HovMax)
        {
            GoDown = true;
        }
        else if (gameObject.transform.position.y <= HovMin)
        {
            GoDown = false; 
        }

        if (gameObject.transform.position.y <= HovMax && GoDown == false)
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);          
        }
        else if (gameObject.transform.position.y >= HovMin && GoDown == true)
        {
            gameObject.transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        gameObject.transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        PowerUpAttributes Racer = other.gameObject.GetComponent<PowerUpAttributes>();

        if ((Racer.CompareTag("Player") || Racer.CompareTag("Unit")) && gameObject.CompareTag("HealthBoostPU") && Racer.SpeedBoostPU == false)
        {
            gameObject.SetActive(false);
            Racer.HealthIncPU = true;
        }
        else if ((Racer.CompareTag("Player") || Racer.CompareTag("Unit")) && gameObject.CompareTag("SpeedBoostPU") && Racer.HealthIncPU == false)
        {
            gameObject.SetActive(false);
            Racer.SpeedBoostPU = true;
        }


    }
}


