using UnityEngine;
using Assets.Scripts;
using System.Collections;
using System;

public class Spear : MonoBehaviour, Weapons
{
    private int m_damage = 1;

    public int damage
    {
        get { return m_damage; }
        set { m_damage = value; }
    }

    private bool collision = false;

    void Update()
    {
        if (collision == true)
        {
            GetComponent<Spear>().damage = GetComponent<Unit>().health;
        }
    }
}
