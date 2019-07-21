using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersonalSoundtrackManager : MonoBehaviour
{
    Camera mc;
    public AudioClip[] songs;
    AudioSource audioSource;
    int currentSong;

    // Use this for initialization
    void Start()
    {
        mc = Camera.main;
        audioSource = GetComponent<AudioSource>();
        currentSong = -1;
        UpdateMusic(SceneManager.GetActiveScene().buildIndex);
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            transform.position = mc.gameObject.transform.position;
        }
        catch (MissingReferenceException)
        {
            mc = Camera.main;
            transform.position = mc.gameObject.transform.position;
        }
        Debug.Log("Soundtrack" + SceneManager.GetActiveScene().buildIndex);
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
            case 1:
                UpdateMusic(0);
                break;
            case 2:
                UpdateMusic(1);
                break;
        }
    }

    void UpdateMusic(int index)
    {
        if (index != currentSong)
        {
            audioSource.Stop();
            currentSong = index;
            audioSource.clip = songs[currentSong];
            audioSource.Play();
        }
    }
}
