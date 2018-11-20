using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    
    public GameObject gameOverUI;
    public Text text;

    public bool gameOver = false;

    // Update is called once per frame
    void Update () {
        if (HealthManager.health == 0) {
            if (!gameOver) {
                gameOverUI.SetActive(true);
                Time.timeScale = 0f;
                DisplayScore();
                ScoreManager.instance.SaveScore();
                gameOver = true;
            }
        }

    }

    public void DisplayScore() {
        text.text = "Your score: " + ScoreManager.instance.score;
    }

    public void LoadMenu() {
        Debug.Log("Loading menu...");
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
