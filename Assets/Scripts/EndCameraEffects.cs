using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndCameraEffects : MonoBehaviour {
    public GameObject track;
    public Text endText;
    bool finished = false;
    int placeVal;

    void Awake()
    {
        RaceManager RM = GameObject.Find("RaceManager").GetComponent<RaceManager>();
        placeVal = (RM.UnitList.IndexOf(RM.player.gameObject) + 1);
    }

	void Update () {
        transform.LookAt(track.transform);
        transform.RotateAround(track.transform.position, track.transform.up, Time.deltaTime * 30);

        if(finished == false)
        {          
            if (placeVal != 1)
            {
                endText.text = "You are number " + placeVal.ToString() + ". Better luck next time.";
            }

            else
                endText.text = "Congratulations!! You finished First!!";

            finished = true;
        }
    }
}
