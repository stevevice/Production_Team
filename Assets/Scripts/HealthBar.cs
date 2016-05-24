using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public float currentHP;
    public Slider healthbar;

    public UnitAttributes Player;
    UnitAttributes AI;

    public void takedamage(float amount)
    {
        currentHP -= amount;
        currentHP = Player.health;
        healthbar.value = currentHP;
    }

    void Update()
    {
        healthbar.value = Player.health;
        transform.LookAt(Player.transform.up);
    }
}