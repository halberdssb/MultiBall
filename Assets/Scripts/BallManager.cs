using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/*
 * Handles ball spawning and ball values in game
 *
 * Jeff Stevenson
 * 4.14.26
 */
public class BallManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField] private float defaultBallSpeed = 3;
    [SerializeField] private float defaultMoneyGainedOnBounce = 0.1f;
    [SerializeField] private float speedIncreasePercentage = 0.2f; // percentage of default speed will be increased by with each upgrade
    [SerializeField] private float moneyGainedIncreasePerPurchase = .05f; // amount of money added to bounce each purchase

    private List<Ball> balls = new List<Ball>();

    public int CurrentNumBalls
    {
        get
        {
            return _currentNumBalls;
        }
        set
        {
            _currentNumBalls = value;
            OnNumBallsChanged?.Invoke(value);
        }
    }
    public float CurrentBallSpeed
    {
        get
        {
            return _currentBallSpeed;
        }
        set
        {
            _currentBallSpeed = value;
            OnBallSpeedChanged?.Invoke(value);
        }
    }

    public float CurrentMoneyGainedOnBounce
    {
        get
        {
            return _currentMoneyGainedOnBounce;
        }
        set
        {
            _currentMoneyGainedOnBounce = value;
            OnMoneyGainedOnBounceChanged?.Invoke(_currentMoneyGainedOnBounce);
        }
    }
    
    public delegate void OnNumBallsChangedHandler(int newNumBalls);
    public static OnNumBallsChangedHandler OnNumBallsChanged;
    
    public delegate void OnBallSpeedChangedHandler(float newBallSpeed);
    public static OnBallSpeedChangedHandler OnBallSpeedChanged;
    
    public delegate void OnMoneyGainedOnBounceChangedHandler(float newAmountEarnedOnBounce);
    public static OnMoneyGainedOnBounceChangedHandler OnMoneyGainedOnBounceChanged;

    private int _currentNumBalls;
    private float _currentBallSpeed;
    private float _currentMoneyGainedOnBounce;
    
    private void Awake()
    {
        CurrentBallSpeed = defaultBallSpeed;
        CurrentMoneyGainedOnBounce = defaultMoneyGainedOnBounce;
    }

    private void Start()
    {
        GameStateManager.OnGameStarted += SpawnBall;
        GameStateManager.OnGameEnded += DestroyAllBalls;
        GameStateManager.OnGameEnded += ResetBallData;
    }
    
    public void SpawnBall()
    {
        SpawnBall(CollisionBounds.CenterOfBounds, CurrentBallSpeed, CurrentMoneyGainedOnBounce);
    }

    public void SpawnBall(Vector3 spawnPos, float spawnSpeed, float spawnMoneyGainedOnBounce)
    {
        GameObject ballObject = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        Ball spawnedBall = ballObject.GetComponent<Ball>();
        
        spawnedBall.Speed = spawnSpeed;
        spawnedBall.MoneyGainedOnBounce = spawnMoneyGainedOnBounce;
        
        OnBallSpeedChanged += spawnedBall.UpdateSpeed;
        OnMoneyGainedOnBounceChanged += spawnedBall.UpdateMoneyGainedOnBounce;

        CurrentNumBalls++;

        balls.Add(spawnedBall);
    }

    public void IncreaseBallSpeed()
    {
        CurrentBallSpeed += defaultBallSpeed * speedIncreasePercentage;
    }

    public void IncreaseMoneyGainedPerBounce()
    {
        CurrentMoneyGainedOnBounce += moneyGainedIncreasePerPurchase;
    }

    public void DestroyAllBalls()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            OnBallSpeedChanged -= balls[i].UpdateSpeed;
            OnMoneyGainedOnBounceChanged -= balls[i].UpdateMoneyGainedOnBounce;
            Destroy(balls[i].gameObject);
        }
        
        balls.Clear();
    }

    public void ResetBallData()
    {
        CurrentNumBalls = 0;
        CurrentBallSpeed = defaultBallSpeed;
        CurrentMoneyGainedOnBounce = defaultMoneyGainedOnBounce;
    }
}
