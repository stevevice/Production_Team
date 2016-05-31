using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Music : MonoBehaviour
{
    private static Music _play;

    public static Music play
    {
        get
        {
            if (_play == null)
            {
                _play = GameObject.FindObjectOfType<Music>();
                DontDestroyOnLoad(_play.gameObject);
            }
            return _play;
        }
    }

    void Awake()
    {
        if (_play == null)
        {
            _play = this;
            
        }
    }

}
