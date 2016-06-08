using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {

    public float timeAlive;
    float aliveTill;

	// Use this for initialization
	void Start () {
        aliveTill = Time.time + timeAlive;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= aliveTill)
            Destroy(gameObject);
	}
}
