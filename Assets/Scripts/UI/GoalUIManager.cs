using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalUIManager : MonoBehaviour
{
    [SerializeField] private Slider goalProgressMeter;
    
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.OnScoreChanged += UpdateGoalProgressUI;
        GoalManager.OnGoalChanged += UpdateUIForNewGoal;
    }
    
    // updates the meter and text for goal progress
    private void UpdateGoalProgressUI(int scoreChangedAmount, int newScore)
    {
        goalProgressMeter.value = newScore;
    }
    
    // updates the meter and text for the new goal score target
    private void UpdateUIForNewGoal(int newGoalNumber, int newScoreTarget)
    {
        goalProgressMeter.maxValue = newScoreTarget;
        goalProgressMeter.value = 0;
    }
}
