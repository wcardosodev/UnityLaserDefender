using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource audi;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            print("Destroying duplicate music player");
        }
        else
        {
            instance = this;
            audi = instance.GetComponent<AudioSource>();
            audi.clip = startClip;
            audi.loop = true;
            audi.Play();
            DontDestroyOnLoad(instance);
        }
    }


    void OnLevelWasLoaded(int level)
    {
        Debug.Log("MusicPlayerWorking");

        if(level == 0)
        {
            try
            {
                audi.clip = startClip;
                audi.volume = 0.75f;
            }
            catch (NullReferenceException e)
            {
                Debug.Log(e.Message);
            }
        }
        if (level == 1)
        {
            audi.clip = gameClip;
            audi.volume = 0.05f;
        }
        if (level == 2)
        {
            audi.clip = endClip;
            audi.volume = 0.75f;
        }
        try
        {
            audi.loop = true;
            audi.Play();
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e.Message);
        }
    }
}
