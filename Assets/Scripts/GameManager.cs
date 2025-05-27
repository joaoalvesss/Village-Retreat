using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using TMPro; 

public class GameManager : MonoBehaviour
{
    public GridManager gridManager;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winText; 

    private float elapsedTime;
    private bool isRunning = true;
    private bool isPaused = false;

    void Start()
    {
        elapsedTime = 0f;
        isRunning = true;
        if (winText != null) winText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (isRunning && !isPaused)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }

        if (isRunning && gridManager.AreAllTargetsCovered())
        {
            isRunning = false;
            ShowWinAndLoad();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        Debug.Log(isPaused ? "II Game Paused" : "-> Game Resumed");
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    void ShowWinAndLoad()
    {
        Debug.Log($"Puzzle Solved in {elapsedTime:F0} seconds!");
        if (winText != null) winText.gameObject.SetActive(true);
        Invoke(nameof(LoadNextScene), 5f);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Island");
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }
}
