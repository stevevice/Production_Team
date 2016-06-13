using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPlacement : MonoBehaviour
{
    public Text pos;

    public RaceManager position;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        pos.text = (position.UnitList.IndexOf(player) + 1).ToString();
    }
}
