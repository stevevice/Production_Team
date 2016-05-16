using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Select_Track : MonoBehaviour
{	
	void Update ()
    {
        if (Input.GetMouseButton(0))
            SceneManager.LoadScene("Track 1");

        if (Input.GetMouseButton(1))
            SceneManager.LoadScene("Track 2");

        if (Input.GetMouseButton(2))
            SceneManager.LoadScene("Track 3");
    }
}
