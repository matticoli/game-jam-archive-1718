using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanic : MonoBehaviour {

    int maxLight;
    bool isHidden;
    bool isLeft;
    int hiddenness;
    bool isHoldingRock;
    int light;
    LightMeter lightMeter;
    GameObject hiddenShadow;
    GameObject pushHitBox;
    public GameObject deathExplosion;
    AudioSource audioSource;
    CompanionController companionController;

	// Use this for initialization
	void Start ()
    {
        maxLight = 1000;
        light = maxLight;
        isHidden = false;
        isLeft = false;
        lightMeter = FindObjectOfType<LightMeter>();
        hiddenShadow = GameObject.FindGameObjectWithTag("HiddenShadow");
        hiddenShadow.SetActive(isHidden);
        pushHitBox = GameObject.FindGameObjectWithTag("PushHitBox");
        pushHitBox.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        companionController = FindObjectOfType<CompanionController>();
        Debug.Log("starting");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.H) != isHidden)
        {
            isHidden = Input.GetKey(KeyCode.H);
            hiddenShadow.SetActive(isHidden);
            hiddenness = 512 * (isHidden?1:0);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine("Push");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            isLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            isLeft = false;
        }
        if (isHidden)
        {
            if (hiddenness > 1)
            {
                hiddenness /= 2;
            }
            else
            {
                hiddenness = 1;
            }
            hiddenShadow.transform.localScale = new Vector3(hiddenness, hiddenness);
        } else
        {
            hiddenness = 0;
        }
        if (++light > maxLight) light = maxLight;
        if (light <= 0)
        {
            try
            {
                GameObject.Instantiate(deathExplosion, companionController.gameObject.transform);
                Destroy(companionController.gameObject);
                maxLight = 1000;
                light = maxLight;
            } catch (MissingReferenceException)
            {
                Debug.Log("Dead");
            }
        }
    }

    void Damage ()
    {
        light-=1;
        lightMeter.SendMessage("SetLight", light);
    }

    public bool GetHidden ()
    {
        return isHidden;
    }

    IEnumerator Push()
    {
        pushHitBox.SetActive(true);
        pushHitBox.SendMessage("SetDirection", isLeft, SendMessageOptions.DontRequireReceiver);
        audioSource.Play();
        maxLight-=20;
        yield return new WaitForSeconds(0.5f);
        pushHitBox.SetActive(false);
    }
}
