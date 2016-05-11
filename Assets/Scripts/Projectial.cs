using UnityEngine;
using Assets.Scripts;
using System.Collections;

public class Projectial : MonoBehaviour, Weapons
{
    public int m_damage;

    float speed;

    int Weapons.damage
    {
        get { return m_damage; }
    }
    
    private bool shoot = false;

    void Update ()
    {
        m_damage = 3;

        if (Input.GetButtonDown("Fire1"))
        {
            shoot = true;
            if (shoot == true)
            {
                speed = 15;
                speed -= .001f * Time.deltaTime;
            }
        }
	}
}