using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    
    // Start is called before the first frame update
    void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();

        ScoreManager.OnScoreChanged += UpdateScoreDisplay;
        
        // set starting score display to 0
        UpdateScoreDisplay(0,0);
    }

    private void UpdateScoreDisplay(int changeAmount, int newScore)
    {
        _scoreText.text = "Score: " + newScore;
    }
}
