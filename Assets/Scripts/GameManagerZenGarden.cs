using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;


public class GameManagerZenGarden : MonoBehaviour
{
    public GridManager gridManager;
    public TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool isRunning = true;
    private bool isPaused = false;
    public GameObject winPanel;
    public TextMeshProUGUI winMessageText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI feedbackText;
    public static GameManagerZenGarden Instance { get; private set; }
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public HashSet<Vector2Int> alreadyScored = new();
    public TextMeshProUGUI finalScoreText;
    public Button giveUpButton;
    public List<float> whiteOnGreenTimes = new();

    void Start()
    {
        elapsedTime = 0f;
        isRunning = false;
        UpdateScoreUI();
        
        if (giveUpButton != null)
            giveUpButton.onClick.AddListener(ForceEndGame);

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

            if (gridManager.AreAllTargetsCovered())
            {
                isRunning = false;
                
                int matchedCount = 0;
                foreach (TargetTile target in FindObjectsByType<TargetTile>(FindObjectsSortMode.None))
                {
                    if (gridManager.potPositions.TryGetValue(target.gridPos, out GameObject potObj))
                    {
                        Pot pot = potObj.GetComponent<Pot>();
                        if (pot != null && gridManager.DoesPotMatchTarget(pot.potType, target.targetType))
                            matchedCount++;
                    }
                }
                AddScore(matchedCount * 10);

                ShowWinAndLoad();
            }
        }
    }

    void Awake()
    {
        Instance = this;
    }

    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f;
    }

    public void ForceEndGame()
    {
        if (!isRunning) return;

        isRunning = false;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
            if (winMessageText != null)
                winMessageText.text = "GAME ENDEDED!";

            if (finalScoreText != null)
                finalScoreText.text = "FINAL SCORE : " + score;

            StartCoroutine(CountdownToNextScene());
        }
    }

    public void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.gameObject.SetActive(true);
            CancelInvoke(nameof(HideFeedback));
            Invoke(nameof(HideFeedback), 5f);
        }
    }

    public void ShowPotDestroyedMessage()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "A pot sunk in the water \n Be careful!";
            feedbackText.gameObject.SetActive(true);
            Invoke(nameof(HideFeedback), 5f);
        }

        SubtractScore(5);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void SubtractScore(int amount)
    {
        score = Mathf.Max(0, score - amount);
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score.ToString();
    }

    void HideFeedback()
    {
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
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

    void AwardTimeBonus()
    {
        int timeBonus = Mathf.Max(0, 180 - Mathf.FloorToInt(elapsedTime));
        AddScore(timeBonus);
    }

    void ShowWinAndLoad()
    {
        if (winPanel != null)
        {
            AddScore(100); 
            AwardTimeBonus(); 
            winPanel.SetActive(true);
            if (winMessageText != null) winMessageText.text = "YOU WON!";
            if (finalScoreText != null) finalScoreText.text = "FINAL SCORE : " + score;
            StartCoroutine(CountdownToNextScene());
        }
    }

    System.Collections.IEnumerator CountdownToNextScene()
    {
        int seconds = 30;
        while (seconds > 0)
        {
            if (countdownText != null)
                countdownText.text = $"Going back to island in {seconds} seconds!";
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