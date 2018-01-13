using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;

    public AudioClip startClip, mainGameClip, endClip;
    AudioClip gameClip;

    private AudioSource audi;

    void Start()
    {
        gameClip = mainGameClip;
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
            audi.volume = .5f;
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

    public void SetGameClip(AudioClip clip)
    {
        gameClip = clip;
        audi.Play();
    }

    public void ResetGameClip()
    {
        gameClip = mainGameClip;
        audi.Play();
    }
}
