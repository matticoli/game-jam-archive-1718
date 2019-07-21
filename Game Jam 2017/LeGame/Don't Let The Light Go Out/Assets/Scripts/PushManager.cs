using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushManager : MonoBehaviour {

    bool pushLeft;

	// Use this for initialization
	void Start () {
        //pushLeft = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            other.SendMessage("GetPushed", pushLeft, SendMessageOptions.DontRequireReceiver);
        }
    }

    void SetDirection(bool d)
    {
        pushLeft = d;
    }
}
