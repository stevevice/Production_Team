using UnityEngine;
using Assets.Scripts;
using System.Collections;
using System;

public class Shield : MonoBehaviour, Weapons
{
    private int m_damage;

    public int health = 5;

    public int damage
    {
        get { return m_damage; }
        set { m_damage = 0; }
    }
    public bool shield = true;
    public bool collision = false;
    
    
    void Update ()
    {
        if (health == 0)
        {
            shield = false;
        }
        else
            shield = true;

        if (collision == true)
        {
            GetComponent<Shield>().health = GetComponent<Weapons>().damage;
            health -= damage;
        }
	}
}
