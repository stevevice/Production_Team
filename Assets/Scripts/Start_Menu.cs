using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Start_Menu: MonoBehaviour
{ 
    void Update()
    {
        if (Input.GetButton("Vehicles"))
         SceneManager.LoadScene("Vehicles");

        if (Input.GetButton("Controls"))
            SceneManager.LoadScene("Controls");

        if (Input.GetButton("Back"))
            SceneManager.LoadScene("Start_Menu");
    }
}