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
    }

    public bool shield = true;
    
    void Update ()
    {
        if (health == 0)
        {
            shield = false;
        }
	}
}
