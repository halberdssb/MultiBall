using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Handles the active score goals throughout gameplay
 *
 * Jeff Stevenson
 * 4.14.26
 */

public class GoalManager : MonoBehaviour
{
    [SerializeField] private float baseGoalScoreTarget; // score goal of first goal - each goal target is 2^n-1 * this value

    public static int CurrentGoalScoreTarget { get; private set; } = 0; // score needed to complete the goal
    public static int CurrentGoalNumber { get; private set; } = 0; // what number goal is this in gameplay? (first, second, etc)
    public static int CurrentGoalScore { get; private set; } = 0; // current score earned toward this goal
    public static float GoalTime { get; private set; } = 30; // time (in seconds) the current goal is active
    public static float GoalTimeRemaining { get; private set; } = 0; // how much time (seconds) is left for the current goal
    
    public delegate void OnGoalChangedHandler(int newGoalNumber, int newGoalScoreTarget);
    public static OnGoalChangedHandler OnGoalChanged;

    public delegate void OnGoalFailedHandler();
    public static OnGoalFailedHandler OnGoalFailed;

    private Coroutine _countdownRoutine; // holds the current countdown routine
    void Start()
    {
        ScoreManager.OnScoreChanged += UpdateCurrentGoalScore;

        GameStateManager.OnGameStarted += StartNextGoal;
        GameStateManager.OnGameEnded += ResetGoalData;
    }

    // starts the next goal
    public void StartNextGoal()
    {
        // reset current score and update goal number
        CurrentGoalScore = 0;
        CurrentGoalNumber++;
        Debug.Log("Goal #" + CurrentGoalNumber + " started!");
        
        // update target score for new goal
        int newGoalTargetScore = Mathf.RoundToInt(Mathf.Pow(2, CurrentGoalNumber - 1) * baseGoalScoreTarget);
        CurrentGoalScoreTarget = newGoalTargetScore;
        Debug.Log("New Goal Target: " + CurrentGoalScoreTarget);
        
        // restart countdown
        if (_countdownRoutine != null) StopCoroutine(_countdownRoutine);
        _countdownRoutine = StartCoroutine(GoalTimeCountdown(GoalTime));
        
        // fire goal changed delegate
        OnGoalChanged?.Invoke(CurrentGoalNumber, CurrentGoalScoreTarget);
    }
    
    // update score earned toward this goal
    private void UpdateCurrentGoalScore(int scoreChangeAmount, int newScore)
    {
        CurrentGoalScore += scoreChangeAmount;
        
        // check if goal is reached
        if (HasScoreTargetBeenReached())
        {
            StartNextGoal();
        }
    }
    
    // checks if goal score target has been reached
    private bool HasScoreTargetBeenReached()
    {
        return CurrentGoalScore >= CurrentGoalScoreTarget;
    }
    
    // routine to decrement goal timer
    private IEnumerator GoalTimeCountdown(float countdownGoalTime)
    {
        GoalTimeRemaining = countdownGoalTime;

        // decrement timer each frame
        while (GoalTimeRemaining > 0)
        {
            GoalTimeRemaining -= Time.deltaTime;
            yield return null;
        }
        
        // if timer reaches 0 or below, goal is failed - lose game
        OnGoalFailed?.Invoke();
        Debug.Log("Goal Failed - Game Lost!");
    }

    public static void ResetGoalData()
    {
        CurrentGoalScoreTarget = 0;
        CurrentGoalNumber = 0;
        CurrentGoalScore = 0;
        GoalTimeRemaining = 0;
    }
}
