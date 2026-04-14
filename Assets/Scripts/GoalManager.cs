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
    [SerializeField] private float goalTime; // time (in seconds) the current goal is active
    [SerializeField] private float baseGoalScoreTarget; // score goal of first goal - each goal target is 2^n * this value

    public int CurrentGoalScoreTarget { get; private set; } // score needed to complete the goal
    public int CurrentGoalNumber { get; private set; } // what number goal is this in gameplay? (first, second, etc)
    public int CurrentGoalScore { get; private set; } // current score earned toward this goal

    public delegate void OnGoalChangedHandler(int newGoalNumber, int newGoalScoreTarget);
    public static OnGoalChangedHandler OnGoalChanged;

    private float _goalTimeRemaining; // how much time is left for the current goal
    private Coroutine _countdownRoutine; // holds the current countdown routine
    void Start()
    {
        ScoreManager.OnScoreChanged += UpdateCurrentGoalScore;
    }

    // starts the next goal
    public void StartNextGoal()
    {
        CurrentGoalScore = 0;
        CurrentGoalNumber++;
        int newGoalTargetScore = Mathf.RoundToInt(Mathf.Pow(2, CurrentGoalNumber) * baseGoalScoreTarget);
        SetGoal(newGoalTargetScore);
        _countdownRoutine = StartCoroutine(GoalTimeCountdown(goalTime));
    }
    // starts a current goal at a specified target value
    public void SetGoal(int goalTarget)
    {
        CurrentGoalScoreTarget = goalTarget;
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
        _goalTimeRemaining = countdownGoalTime;

        // decrement timer each frame
        while (_goalTimeRemaining > 0)
        {
            _goalTimeRemaining -= Time.deltaTime;
            yield return null;
        }
        
        // if timer reaches 0 or below, goal is failed - lose game
        Debug.Log("Goal Failed - Game Lost!");
    }
}
