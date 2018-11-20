using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance;

    public int score;
    [HideInInspector]
    public int highScore1;
    [HideInInspector]
    public int highScore2;
    [HideInInspector]
    public int highScore3;

    public Text scoreText;

	// Use this for initialization, Awake happens before Start
	void Awake () {
        score = 0;

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        AudioManager.instance.backgroundMusic = true;
        AudioManager.instance.menuMusic = false;

        highScore1 = PlayerPrefs.GetInt("HighScore1", 0);
        highScore2 = PlayerPrefs.GetInt("HighScore2", 0);
        highScore3 = PlayerPrefs.GetInt("HighScore3", 0);
    }
    
	// Update is called once per frame
	void Update () {
        scoreText.text = score.ToString();
    }

    public void SaveScore() {
        if(score > highScore1) {
            
            PlayerPrefs.SetInt("HighScore2", highScore1);
            PlayerPrefs.SetInt("HighScore3", highScore2); 
            PlayerPrefs.SetInt("HighScore1", score);
        } else if(score > highScore2) {
            PlayerPrefs.SetInt("HighScore3", highScore2);
            PlayerPrefs.SetInt("HighScore2", score);
        } else if(score > highScore3) {
            PlayerPrefs.SetInt("HighScore3", score);
        }
    }
}
