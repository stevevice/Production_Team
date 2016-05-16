using UnityEngine;
using System.Collections;

public class Bullet_Control : MonoBehaviour
{
    public float damage;
    public float speed;
    float pretime;
    float force;
    Vector3 preVector;
    Transform bulletTrans;
    Rigidbody rb;

    Vector3 Force;

	void Start ()
    {
        pretime = 0;
        force = 0;
        preVector = gameObject.transform.position;
        bulletTrans = gameObject.GetComponent<Transform>();
        speed = 0;
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        float timeint = Time.time - pretime;
        Vector3 Dis = gameObject.transform.position - preVector;
        force = Dis.sqrMagnitude / timeint;

        //bulletTrans.Translate(Vector3.forward * speed);
        rb.AddForce(Force * 50);
	}

    public void SetForce(Vector3 f)
    {
        Force = f;
    }
}

