using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManagerZenGarden : MonoBehaviour
{
    public GridManager gridManager;
    public TextMeshProUGUI timerText;
    // public TextMeshProUGUI winText;
    private float elapsedTime;
    private bool isRunning = true;
    private bool isPaused = false;
    public GameObject winPanel;
    public TextMeshProUGUI winMessageText;
    public TextMeshProUGUI countdownText;
    // public AudioSource winSound;

    void Start()
    {
        elapsedTime = 0f;
        isRunning = true;
        // if (winText != null) winText.gameObject.SetActive(false);
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
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            if (winMessageText != null) winMessageText.text = "YOU WIN!";
            // if (winSound != null) winSound.Play();
            StartCoroutine(CountdownToNextScene());
        }
    }

    System.Collections.IEnumerator CountdownToNextScene()
    {
        int seconds = 30;
        while (seconds > 0)
        {
            if (countdownText != null)
                countdownText.text = $"Going back to island in {seconds} seconds! ";
            yield return new WaitForSeconds(1f);
            seconds--;
        }
        LoadNextScene();
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

    public void SkipToNextScene()
    {
        StopAllCoroutines();
        LoadNextScene();
    }

}