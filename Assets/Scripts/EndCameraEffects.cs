using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndCameraEffects : MonoBehaviour {
    public GameObject track;
    public Text endText;
    public GameObject player;
    bool finished = false;

	void Update () {
        transform.LookAt(track.transform);
        transform.RotateAround(track.transform.position, track.transform.up, Time.deltaTime * 30);

        if(finished == false)
        {
            RaceManager RM = GameObject.Find("RaceManager").GetComponent<RaceManager>();
            int placeVal = (RM.UnitList.IndexOf(player) + 1);

            if (placeVal != 1)
            {
                endText.text = "You are number " + placeVal + ". Better luck next time.";
            }

            else
                endText.text = "Congratulations!! You finished First!!";

            finished = true;
        }
    }
}
