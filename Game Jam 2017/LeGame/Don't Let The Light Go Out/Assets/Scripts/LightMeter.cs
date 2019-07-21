using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightMeter : MonoBehaviour {

    int light;
    const int natColorLight = 1000;
    RawImage img;

	// Use this for initialization
	void Start () {
        img = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetLight (int l)
    {
        light = l;
        UpdateLight();
    }

    void UpdateLight()
    {
        img.color = new Color((float)light/(float)natColorLight, (float)light / (float)natColorLight, (float)light / (float)natColorLight);
    }
}
