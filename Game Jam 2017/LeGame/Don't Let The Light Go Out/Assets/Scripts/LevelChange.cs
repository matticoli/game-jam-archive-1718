using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int lvlNum = 0;
        int.TryParse(currentScene.name.Split(' ')[1], out lvlNum);
        SceneManager.LoadScene("Level " + (lvlNum+1));
    }
}
