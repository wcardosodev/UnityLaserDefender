using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    static LevelManager level = null;

    private void Start()
    {
        if(level != null && level != this)
        {
            Destroy(gameObject);
            print("Destroying duplicate Level manager");
        }
        else
        {
            level = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel(string name)
    {
        Debug.Log(string.Format("Level load requested for {0}", name));
        SceneManager.LoadScene(name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitRequest()
    {
        Debug.Log("I want to Quit");
        Application.Quit();
    }
}
