using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject titleScreen;
    public GameObject instructions;
    
    public int score;
    public Text scoreText;
    
    
    public void UpdateLives(int currentLives)
    {
        Debug.Log("Player lives: " + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }
    
    public void ScoreUp()
    {
        score += 1;

        scoreText.text = "SCORE " + score;
    }
    
    public void ScoreDown()
    {
        if(score >= 1)
        {
            score -= 1;

        }

        scoreText.text = "SCORE " + score;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        instructions.SetActive(true);
    }
    
    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        instructions.SetActive(false);
        score = 0;
    }
}
    
