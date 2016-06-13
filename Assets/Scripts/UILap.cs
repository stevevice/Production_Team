using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILap : MonoBehaviour {

    UnitAttributes Unit; //Player
    public Text lapText;
    RaceManager RM;

	void Start () {
        Unit = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitAttributes>();
        RM = GameObject.Find("RaceManager").GetComponent<RaceManager>();
	}
	
	// Update is called once per frame
	void Update () {
        lapText.text = "Lap: " + (Unit.lap + 1) + "/" + RM.LapsNeed;
	}
}
