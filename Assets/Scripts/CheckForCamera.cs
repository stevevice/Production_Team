using UnityEngine;
using System.Collections;

public class CheckForCamera : MonoBehaviour {

    public GameObject startCamera;
	
	void Awake () {
        if (GameObject.Find("StartCamera(Clone)") != true)
        { 
            Instantiate(startCamera);
        }
	}
}
