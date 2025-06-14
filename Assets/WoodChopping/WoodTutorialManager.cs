using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WoodTutorialManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public LogSpawner logSpawner;

    public Camera tutorialCam;
    public Camera gameplayCam;
    public GameObject tutorialUI;
    public SpeechBalloon player1Balloon;
    public GameObject continuePrompt;

    public Image image1;

    private int step = 0;

    void Start()
    {
        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        tutorialCam.gameObject.SetActive(true);
        tutorialUI.SetActive(true);
        gameplayCam.gameObject.SetActive(false);
        continuePrompt.SetActive(false);
        ShowNextStep();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextStep();
        }
    }

    void ShowNextStep()
    {
        player1Balloon.Hide(image1);
        continuePrompt.SetActive(true);
        
        step++;
        switch (step)
        {
            case 1:
                player1Balloon.Show("Welcome to Timber Time!", image1);
                break;
            case 2:
                player1Balloon.Show("Use e/Enter to cut the logs in the correct positions!", image1);
                break;
            case 3:
                player1Balloon.Show("Each completed log grants you 500 points.", image1);
                break;
            case 4:
                player1Balloon.Show("But beware! If you cut a log in the wrong spot, you lose points.", image1);
                break; 
            case 5:
                player1Balloon.Show("Get as many points as you can in 60 seconds!", image1);
                break;           
            case 6:
                player1Balloon.Show("Good luck!", image1);
                EndTutorial();
                break;
        }
    }

    public void EndTutorial()
    {
        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        tutorialCam.gameObject.SetActive(false);
        tutorialUI.SetActive(false);
        gameplayCam.gameObject.SetActive(true);

        logSpawner.StartTimer(); 
    }
}
