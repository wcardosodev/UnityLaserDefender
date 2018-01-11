using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {

    Slider slider;
	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        try
        {
            slider.value = FindObjectOfType<PlayerController>().HealthAsPercent;
        }
        catch (System.NullReferenceException)
        {
            slider.value = 0;
        }
    }
}
