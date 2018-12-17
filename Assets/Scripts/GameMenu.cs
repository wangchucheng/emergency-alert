using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    public Slider HPSlider;
    public Image HPSliderFill;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (HPSlider.value >= 50)
        {
            HPSliderFill.color = Color.green;
        }
        else
            if (HPSlider.value >= 20)
        {
            HPSliderFill.color = Color.yellow;
        }
        else
            HPSliderFill.color = Color.red;
	}
}
