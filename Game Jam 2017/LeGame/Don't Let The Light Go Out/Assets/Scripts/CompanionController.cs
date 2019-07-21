using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompanionController : MonoBehaviour
{

    int health;
    int floatDir = 1;
    float floatOffset = 0f;
    GameObject target;

    bool home;

    // Use this for initialization
    void Start()
    {
        if(SceneManager.GetActiveScene().name.Equals("Level 2"))
        {
            home = true;
        }
        target = FindObjectOfType<PlayerMechanic>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(home)
        {
            return;
        }

        if (transform.position.x < target.transform.position.x - 2)
        {
            transform.position += new Vector3(Time.deltaTime, 0, 0)* 3f;
        }
        else
        if (transform.position.x > target.transform.position.x + 2)
        {
            transform.position += new Vector3(-Time.deltaTime, 0, 0) * 3f;
        }
        floatOffset += floatDir * Time.deltaTime;
        if (Math.Abs(floatOffset) > 0.5f)
        {
            floatDir *= -1;
        }
        transform.position = new Vector3(transform.position.x, target.transform.position.y + 1f + floatOffset, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        home = false;
    }
}
