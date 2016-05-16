using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Select_Track : MonoBehaviour
{	
	void Update ()
    {
        if (Input.GetButton("Track 1"))
            SceneManager.LoadScene("Track 1");

        if (Input.GetButton("Track 2"))
            SceneManager.LoadScene("Track 2");

        if (Input.GetButton("Track 3"))
            SceneManager.LoadScene("Track 3");
    }
}
