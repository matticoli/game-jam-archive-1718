using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMechanic : MonoBehaviour {

    int health;
    int pushVelocity;
    GameObject target;
    PlayerMechanic targetPlayer;

	// Use this for initialization
	void Start () {
        targetPlayer = FindObjectOfType<PlayerMechanic>();
        target = targetPlayer.gameObject;
        health = 5;
	}
	
	// Update is called once per frame
	void Update () {
        if (!targetPlayer.GetHidden())
        {
            if (transform.position.x < target.transform.position.x - 3)
            {
                transform.position += new Vector3(Time.deltaTime, 0, 0);
            }
            else
            if (transform.position.x > target.transform.position.x + 3)
            {
                transform.position += new Vector3(-Time.deltaTime, 0, 0);
            }
            else
            {
                target.SendMessage("Damage");
            }
        }
        transform.position += new Vector3(((float)pushVelocity)/2048f,0,0);
        pushVelocity /= 2;
    }

    void GetPushed(bool pushLeft)
    {
        pushVelocity += 2048 * (pushLeft ? -1 : 1);
        health--;
        if (health<=0)
        {
            gameObject.SetActive(false);
        }
    }
}
