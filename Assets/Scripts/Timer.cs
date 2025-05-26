using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remaningTime;

    // Update is called once per frame
    void Update()
    {
        if (remaningTime > 0)
        {
            remaningTime -= Time.deltaTime;
        }
        else if (remaningTime < 0)
        {
            remaningTime = 0;
            SceneManager.LoadScene("Island", LoadSceneMode.Single); //Load da cena principal se perder
        }
        int minutes = Mathf.FloorToInt(remaningTime / 60);
        int seconds = Mathf.FloorToInt(remaningTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
