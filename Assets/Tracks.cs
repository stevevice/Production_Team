using UnityEngine;
using System.Collections;

public class Tracks: MonoBehaviour
{ 
    void Update()
    {
        if (Input.GetMouseButton())
            Application.LoadLevel("Tracks");
    }
}