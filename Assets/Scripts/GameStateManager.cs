using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public delegate void OnGameStartedHandler();
    public static event OnGameStartedHandler OnGameStarted;
    
    public delegate void OnGameEndedHandler();
    public static event OnGameEndedHandler OnGameEnded;

    private void Start()
    {
        GoalManager.OnGoalFailed += EndGame;
    }
    
    public static void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    private void EndGame()
    {
        OnGameEnded?.Invoke();
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
