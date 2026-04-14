using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Holds and handles score in game
 *
 * Jeff Stevenson
 * 4.14.26
 */
public class ScoreManager : MonoBehaviour
{
    public static int Score { get; private set; }
    
    public delegate void OnScoreChangedHandler(int changeAmount, int newScore);
    public static OnScoreChangedHandler OnScoreChanged;
    
    void Start()
    {
        
    }

    // changes score by a given amount
    public static void AddToScore(int changeAmount)
    {
        Score += changeAmount;
        OnScoreChanged?.Invoke(changeAmount, Score);
    }
}
