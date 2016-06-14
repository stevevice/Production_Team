using UnityEngine;
using System.Collections;

public class CheckBuild : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.LinuxPlayer)
        {
            gameObject.SetActive(true);
        }

        else
            gameObject.SetActive(false);

	}
	
	public void QuitGame()
    {
        Application.Quit();
    }
}
