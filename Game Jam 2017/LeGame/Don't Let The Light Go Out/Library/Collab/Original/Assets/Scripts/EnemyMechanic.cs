using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMechanic : MonoBehaviour {

    int health;
    GameObject target;

	// Use this for initialization
	void Start () {
        target = FindObjectOfType<PlayerMechanic>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < target.transform.position.x-1)
        {
            transform.position += new Vector3(Time.deltaTime,0,0);
        }
        else
        if (transform.position.x > target.transform.position.x+1)
        {
            transform.position += new Vector3(-Time.deltaTime,0,0);
        }
	}
}
