using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour {

    public Text countDown;
    float startTime;
    float waitTime;

    void Awake()
    {
        waitTime = FindObjectOfType<RaceManager>().waitTime;
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time <= (startTime + waitTime))
            countDown.text = Mathf.CeilToInt((startTime + waitTime) - Time.time).ToString();

        else if (Time.time <= startTime + (waitTime + 2))
            countDown.text = "GO!!!";

        else
            gameObject.SetActive(false);
	}
}
