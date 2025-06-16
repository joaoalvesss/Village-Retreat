using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using FMODUnity;

public class WoodUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public GameObject winPanel;
    public TextMeshProUGUI winMessageText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI finalScoreText;

    private int scoreNum = 0;
    public string timeSound = "event:/Music/minigames/countdown_signal";
    public string skipSound = "event:/UI/Skip";
    private bool playedTimeSound = false;

    public void UpdateScore(int score)
    {
        scoreNum = score;
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateTimer(float timeLeft)
    {
        timerText.text = Mathf.CeilToInt(timeLeft).ToString();
        if (Mathf.CeilToInt(timeLeft) == 3 && !playedTimeSound) {
            Debug.Log("3 seconds left!");
            RuntimeManager.PlayOneShot(timeSound, transform.position);
            playedTimeSound = true;
        }
    }

    public void ShowGameOver()
    {
        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            if (winMessageText != null)
                winMessageText.text = "GAME ENDED!";

            if (finalScoreText != null)
                finalScoreText.text = "FINAL SCORE : " + scoreNum.ToString();

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
        SceneManager.LoadScene("Island");
    }

    public void SkipToNextScene()
    {
        RuntimeManager.PlayOneShot(skipSound, transform.position);
        StopAllCoroutines();
        SceneManager.LoadScene("Island");
    }
}
