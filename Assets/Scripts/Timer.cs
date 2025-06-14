using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remaningTime;
    public bool isRunning = true;

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;

        if (remaningTime > 0)
        {
            remaningTime -= Time.deltaTime;
        }
        else if (remaningTime < 0)
        {
            isRunning = false;
            remaningTime = 0;
            timerText.color = new Color(1, 0, 0, 1);
            FindAnyObjectByType<Home>().Invoke("loseScreen", 3);
        }
        int minutes = Mathf.FloorToInt(remaningTime / 60);
        int seconds = Mathf.FloorToInt(remaningTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public void StopTimer()
    {
        isRunning = false;
        timerText.color = new Color(0, 1, 0, 1);
    }

    private void changeScene()
    {
        SceneManager.LoadScene("Island", LoadSceneMode.Single);
    }

}
