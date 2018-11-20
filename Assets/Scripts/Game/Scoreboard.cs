using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

    public GameObject menuUI;
    public GameObject scoreboardUI;

    public Text score1;
    public Text score2;
    public Text score3;

    void Start () {
        AudioManager.instance.bossMusic = false;
        AudioManager.instance.combatMusic = false;
        AudioManager.instance.backgroundMusic = false;
        AudioManager.instance.menuMusic = true;
    }

    public void DisplayScoreboard () {
        menuUI.SetActive(false);
        scoreboardUI.SetActive(true);
        score1.text = "1st Place: " + PlayerPrefs.GetInt("HighScore1", 0);
        score2.text = "2nd Place: " + PlayerPrefs.GetInt("HighScore2", 0);
        score3.text = "3rd Place: " + PlayerPrefs.GetInt("HighScore3", 0);
    }

    public void HideScoreboard () {
        menuUI.SetActive(true);
        scoreboardUI.SetActive(false);
    }

}
