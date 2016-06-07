using UnityEngine;
using System.Collections;

public class PowerUpAction : MonoBehaviour
{
    bool GoDown = false;
    public float speed = .25f;
    float HovMin = 1f;
    float HovMax = 1.5f;
    Vector3 HovCurrent;

    void Start()
    {
        HovCurrent = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (HovCurrent.y >= HovMax)
        {
            GoDown = true;
        }
        else if (HovCurrent.y <= HovMin)
        {
            GoDown = false;
        }

        if (gameObject.transform.position.y <= HovMax && GoDown == false)
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
            HovCurrent = gameObject.transform.position;
        }
        else if (gameObject.transform.position.y >= HovMin && GoDown == true)
        {
            gameObject.transform.Translate(Vector3.down * Time.deltaTime * speed);
            HovCurrent.y = gameObject.transform.position.y;
        }

        gameObject.transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        PowerUpAttributes Racer = new PowerUpAttributes();
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


