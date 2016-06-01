using UnityEngine;
using System.Collections;

public class EndCameraEffects : MonoBehaviour {
    public GameObject track;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(track.transform);
        transform.RotateAround(track.transform.position, track.transform.up, Time.deltaTime * 30);
	}
}
