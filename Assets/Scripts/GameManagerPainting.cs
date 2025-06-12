using TMPro;
using UnityEngine;

public class GameManagerPainting : MonoBehaviour
{
     public TextMeshProUGUI timerText;
     public TextMeshProUGUI scoreText;
     public float maxTime = 180f;
     private float timeRemaining;
     private bool isRunning = false;

     void Start()
     {
          timeRemaining = maxTime;
          isRunning = true;
     }

     void Update()
     {
          if (!isRunning) return;

          timeRemaining -= Time.deltaTime;
          timeRemaining = Mathf.Max(0, timeRemaining); 

          UpdateTimerUI();

          if (timeRemaining <= 0)
          {
               EndGame();
          }
     }

     void UpdateTimerUI()
     {
          int minutes = Mathf.FloorToInt(timeRemaining / 60f);
          int seconds = Mathf.FloorToInt(timeRemaining % 60f);
          timerText.text = $"< TIME > \n {minutes:00}:{seconds:00}";
     }

     void EndGame()
     {
          isRunning = false;
          Debug.Log("Time's up! Game over.");
          // CHANGE LATER
     }
    

     private int score = 0;

     public void AddScore(int value)
     {
     score += value;
     UpdateScoreUI();
     }

     void UpdateScoreUI()
     {
     if (scoreText != null)
          scoreText.text = $"< SCORE > \n {score}";
     }

}
