using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] currentScoreText;
    public Text[] highScoreText;

	// Use this for initialization
	void Start () {

        foreach(Text t in highScoreText)
        {
            t.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }

        int currentScore = ScoreKeeper.score;

        foreach(Text t in currentScoreText)
        {
            t.text = currentScore.ToString();
        }

        if(currentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", ScoreKeeper.score);
        }
	}
}
