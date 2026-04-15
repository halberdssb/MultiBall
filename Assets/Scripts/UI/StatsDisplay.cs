using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numBallsText;
    [SerializeField] private TextMeshProUGUI ballSpeedText;
    [SerializeField] private TextMeshProUGUI moneyGainedOnBounceText;
    void Start()
    {
        BallManager.OnNumBallsChanged += UpdateNumBallsText;
        BallManager.OnBallSpeedChanged += UpdateBallSpeedText;
        BallManager.OnMoneyGainedOnBounceChanged += UpdateMoneyGainedOnBounceText;
    }

    private void UpdateNumBallsText(int newNumBalls)
    {
        numBallsText.text = "Balls: " + newNumBalls;
    }
    
    private void UpdateBallSpeedText(float newBallSpeed)
    {
        ballSpeedText.text = "Ball Speed: " + newBallSpeed.ToString("F2");
    }
    
    private void UpdateMoneyGainedOnBounceText(float newMoneyGainedOnBounce)
    {
        moneyGainedOnBounceText.text = "Money Earned On Bounce: $" + newMoneyGainedOnBounce.ToString("F2");
    }
}
