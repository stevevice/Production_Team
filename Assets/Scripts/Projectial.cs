using UnityEngine;
using Assets.Scripts;
using System.Collections;

public class Projectial : MonoBehaviour, Weapons
{
    private int m_damage;

    float speed;

    public int damage
    {
        get { return m_damage; }
        set { m_damage = value; }
    }

    private bool collision = false;

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

        if (collision == true)
        {
            GetComponent<Projectial>().damage = GetComponent<Unit>().health;
        }

	}
}
