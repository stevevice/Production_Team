using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour {

    public Text countDown;
	
	// Update is called once per frame
	void Update () {
        if (3 - Time.time >= 0)
            countDown.text = Mathf.CeilToInt(3 - Time.time).ToString();

        else if (Time.time <= 5)
            countDown.text = "GO!!!";

        else
            gameObject.SetActive(false);
	}
}
