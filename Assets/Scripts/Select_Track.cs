using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Select_Track : MonoBehaviour
{
    public void LevelTest(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void TwistyTrack(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Track2(string name)
    {
        SceneManager.LoadScene(name);
    }


}
