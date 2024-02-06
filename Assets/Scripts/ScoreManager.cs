using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    // state
    int score = 0;
    
    public void IncreaseScore() {
        score += 1;
        UpdateScore();
    }

    public void DecreaseScore() {
        score -= 1;
        UpdateScore();
    }

    private void UpdateScore() {
        scoreText.text = "Player 1: " + score;
    }
    
}
