using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] congratulations;
    public Text[] currentScoreText;
    public Text[] highScoreText;

	// Use this for initialization
	void Start () {

        int currentScore = ScoreKeeper.score;

        foreach(Text t in currentScoreText)
        {
            t.text = currentScore.ToString();
        }

        if(currentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", ScoreKeeper.score);

            foreach (Text t in congratulations)
            {
                t.text = "Congratulations Defender";
            }
        }
        else
        { 
            foreach (Text t in congratulations)
            {
                t.text = "You need more practice...";
            }
        }

        foreach (Text t in highScoreText)
        {
            t.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
    }
}
