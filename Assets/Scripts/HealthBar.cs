using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public class ShowHealth : UnityEvent
    {

    }

    public int Health;
    public static ShowHealth fall;

    void Awake()
    {
        if (fall == null)
            fall = new ShowHealth();
    }

    public int takedam;
    public GameObject healthBar;

    void Start()
    {
        Health = 100;
    }
    
    void Update()
    {             

    }
}
