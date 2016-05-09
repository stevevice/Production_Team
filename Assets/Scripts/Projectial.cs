using UnityEngine;
using System.Collections;

public class Projectial : MonoBehaviour
{
    public int Damage = 3;
    float speed = 15;

    private bool shoot = false;

    void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot = true;
        }
	}
}
