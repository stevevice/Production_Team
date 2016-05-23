using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public float hp = 100;
    public float currentHP;
    public Slider healthbar;

    private bool dead;
    private bool damaged = false;

    public UnitAttributes Player;

    public void takedamage(float amount)
    {
        damaged = true;
        currentHP -= amount;
        currentHP = Player.health;
        healthbar.value = currentHP;
        if (currentHP == 0 && !dead)
        {
            died();
        }
    }

    public void died()
    {
        dead = true;
    }

    void Update()
    {
        healthbar.value = Player.health;
    }
}