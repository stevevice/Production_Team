using UnityEngine;
using System.Collections;

public class CheckPointHighlight : MonoBehaviour {

    GameObject currentParticles;
    RaceManager RM;         //Referance to the Game Manager
    UnitAttributes unitAt;  //The Attributes of this Unit.
    public GameObject particles;
    Quaternion parRotation;

	void Start () {
        RM = FindObjectOfType(typeof(RaceManager)) as RaceManager;
        unitAt = gameObject.GetComponent<UnitAttributes>();
        unitAt.nextPoint = RM.Checkpoints[0];
        parRotation = particles.transform.rotation;

        currentParticles = (GameObject)Instantiate(particles, new Vector3(unitAt.nextPoint.transform.position.x, unitAt.nextPoint.transform.position.y - 1, unitAt.nextPoint.transform.position.z), parRotation);
    }
	
	void LateUpdate () {

        if (unitAt.nextPoint.CheckPosition(gameObject))
        {
            if (currentParticles != null)
                Destroy(currentParticles);

            currentParticles = (GameObject)Instantiate(particles, new Vector3(unitAt.nextPoint.transform.position.x, unitAt.nextPoint.transform.position.y - 1, unitAt.nextPoint.transform.position.z), parRotation);
        }
    }
}
