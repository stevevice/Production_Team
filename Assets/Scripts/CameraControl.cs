using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour {

    private static Music _play;
    GameObject Track;

    // Use this for initialization
    void Start () {
        Track = transform.FindChild("Enviornment").gameObject;
        _play = gameObject.GetComponentInChildren<Music>();
        DontDestroyOnLoad(this);
    }

    public void OnLevelWasLoaded(int level)
    {
        if (level >= 4)
        {
            GameObject.FindObjectOfType<Music>();
            _play.gameObject.SetActive(false);
        }

        else
        {
            _play.gameObject.SetActive(true);
        }

        if (SceneManager.GetActiveScene().name != "Start_Menu" && SceneManager.GetActiveScene().name != "Controls")
            Track.SetActive(false);
        else
            Track.SetActive(true);
    }
}
