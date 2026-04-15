using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalUIManager : MonoBehaviour
{
    [SerializeField] private Slider goalProgressMeter;
    [SerializeField] private Slider goalTimeMeter;
    [SerializeField] private TextMeshProUGUI goalProgressText;
    [SerializeField] private TextMeshProUGUI goalTimeText;
    [SerializeField] private TextMeshProUGUI goalTargetText;
    
    void Awake()
    {
        ScoreManager.OnScoreChanged += UpdateGoalProgressUI;
        GoalManager.OnGoalChanged += UpdateUIForNewGoal;

        goalTimeMeter.maxValue = GoalManager.GoalTime;
    }

    void Update()
    {
        goalTimeMeter.value = GoalManager.GoalTimeRemaining;
        goalTimeText.text = GoalManager.GoalTimeRemaining.ToString("F2") + " seconds remaining";
    }
    
    // updates the meter and text for goal progress
    private void UpdateGoalProgressUI(int scoreChangedAmount, int newScoreThisGoal)
    {
        goalProgressMeter.value += scoreChangedAmount;
        goalProgressText.text = goalProgressMeter.value + " / " + GoalManager.CurrentGoalScoreTarget;
    }
    
    // updates the meter and text for the new goal score target
    private void UpdateUIForNewGoal(int newGoalNumber, int newScoreTarget)
    {
        goalProgressMeter.maxValue = newScoreTarget;
        goalProgressMeter.value = 0;
        UpdateGoalProgressUI(0, 0);
        
        goalTargetText.text = "Goal #" + newGoalNumber + ": " + newScoreTarget;
    }
}
