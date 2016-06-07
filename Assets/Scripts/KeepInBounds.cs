using UnityEngine;
using System.Collections;

public class KeepInBounds : MonoBehaviour {

    [HideInInspector]
    public Vector3 origin;
    Renderer unit;

    public bool res;

	// Use this for initialization
	void Start () {
        unit = gameObject.transform.FindChild("Model").transform.FindChild(gameObject.name).GetComponent<Renderer>();
        origin = gameObject.transform.position;
	}

    // Update is called once per frame
    void Update() {
        if (unit.isVisible == false)
        {
            gameObject.transform.position = origin;
            gameObject.GetComponent<Player_Move>().speed = 0;
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
       
    }
}
