using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanic : MonoBehaviour {

    int maxLight;
    public int light;
    public LightMeter lightMeter;

	// Use this for initialization
	void Start () {
        lightMeter = FindObjectOfType<LightMeter>();
        maxLight = 1000;
        light = maxLight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Damage()
    {
        light--;
        lightMeter.SendMessage("SetLight", light);
    }
}
