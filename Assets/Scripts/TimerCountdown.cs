using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour {

    public Text countDown;
    float startTime;

    void Awake()
    {
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time <= (startTime + 3))
            countDown.text = Mathf.CeilToInt((startTime + 3) - Time.time).ToString();

        else if (Time.time <= startTime + 5)
            countDown.text = "GO!!!";

        else
            gameObject.SetActive(false);
	}
}
