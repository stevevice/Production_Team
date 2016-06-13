using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour {

    PowerUpAttributes playerPowerAtt;
    RawImage image;
    public List<Texture> powerUpPictures;

	// Use this for initialization
	void Start () {
        playerPowerAtt = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpAttributes>();
        image = gameObject.GetComponent<RawImage>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerPowerAtt.HealthIncPU == true)
        {
            SelectPicture("Health_Icon");
        }

        else if (playerPowerAtt.SpeedBoostPU == true)
        {
            SelectPicture("Boost_Power_Up_Image");
        }

        else
            SelectPicture("Blank");
	}

    void SelectPicture(string name)
    {
        foreach (Texture t in powerUpPictures)
        {
            if (t.name == name) {
                image.texture = t;
            }
        }
    }
}
