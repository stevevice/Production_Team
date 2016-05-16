using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class selectVehicle : MonoBehaviour
{
    public GameObject Car { get; private set; }
    public GameObject Van { get; private set; }
    public Scene nextScene;

    void Update ()
    {
        if (Input.GetButton("Car"))
        {
            SceneManager.MoveGameObjectToScene(Car, nextScene);
            SceneManager.LoadScene("Tracks");
        }
        if (Input.GetButton("Van"))
        {
            SceneManager.MoveGameObjectToScene(Van, nextScene);
            SceneManager.LoadScene("Tracks");
        }
    }
}
