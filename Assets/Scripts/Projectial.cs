using UnityEngine;
using Assets.Scripts;
using System.Collections;

public class Projectial : MonoBehaviour, Weapons
{
    public int m_damage;
    public GameObject bullet;
    public Transform bulletspawn;
    float speed;
   

    float nextfire;
    public float firerate;

    void Start()
    {
        nextfire = 0;
    }

    int Weapons.damage
    {
        get { return m_damage; }
    }
    
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextfire)
        {
            GameObject temp;
            temp = Instantiate(bullet, bulletspawn.transform.position, new Quaternion()) as GameObject;
            temp.GetComponent<Bullet_Control>().SetForce(transform.forward);

            nextfire = Time.time + firerate;

        }    
	}
}