using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour {

    public GameObject mainCamera;
    public CameraControl cameraControl;
    public float startX, endX, rate;

	// Use this for initialization
	void Start () {
        cameraControl = mainCamera.GetComponent<CameraControl>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(
            (endX - startX) * (mainCamera.transform.position.x-cameraControl.minX) / (cameraControl.maxX-cameraControl.minX) + startX,
            transform.position.y,
            transform.position.z);
	}
}
