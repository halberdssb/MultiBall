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
    
    // Update Call Protection -GS
    private bool _goalActive = false;
    private float _lastDisplayedTime = -1f;

    void Awake()
    {
        ScoreManager.OnScoreChanged += UpdateGoalProgressUI;
        GoalManager.OnGoalChanged += UpdateUIForNewGoal;
        // Stopping continued Update draw calls after game ends - GS
        GameStateManager.OnGameEnded += () => _goalActive = false;

        goalTimeMeter.maxValue = GoalManager.GoalTime;
    }

    void Update()
    {
        if (!_goalActive) return;

        float remaining = GoalManager.GoalTimeRemaining;
        goalTimeMeter.value = remaining;

        // Only rebuild the string when the displayed value would change (every 0.01s) -GS
        float rounded = Mathf.Round(remaining * 100f) / 100f;
        if (!Mathf.Approximately(rounded, _lastDisplayedTime))
        {
            _lastDisplayedTime = rounded;
            goalTimeText.text = rounded.ToString("F2") + " seconds remaining";
        }
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
        // Reset Update Draw Call protections -GS
        _goalActive = true;
        _lastDisplayedTime = -1f;
        goalProgressMeter.maxValue = newScoreTarget;
        goalProgressMeter.value = 0;
        UpdateGoalProgressUI(0, 0);
        
        goalTargetText.text = "Goal #" + newGoalNumber + ": " + newScoreTarget;
    }
}
