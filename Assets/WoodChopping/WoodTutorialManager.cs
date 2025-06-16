using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

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
    public string skipSound = "event:/UI/Skip";
    public string contSound = "event:/UI/UI_hover";

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
                RuntimeManager.PlayOneShot(contSound, transform.position);
                break;
            case 2:
                player1Balloon.Show("Use e/Enter to cut the logs in the correct positions!", image1);
                RuntimeManager.PlayOneShot(contSound, transform.position);
                break;
            case 3:
                player1Balloon.Show("Each completed log grants you 50 points.", image1);
                RuntimeManager.PlayOneShot(contSound, transform.position);
                break;
            case 4:
                player1Balloon.Show("But beware! If you cut a log in the wrong spot, you lose points.", image1);
                RuntimeManager.PlayOneShot(contSound, transform.position);
                break; 
            case 5:
                player1Balloon.Show("Get over 400 points in 60 seconds!", image1);
                RuntimeManager.PlayOneShot(contSound, transform.position);
                break;           
            case 6:
                player1Balloon.Show("Good luck!", image1);
                break;      
            case 7:
                EndTutorial();
                break;
        }
    }

    public void EndTutorial()
    {
        RuntimeManager.PlayOneShot(skipSound, transform.position);
        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        tutorialCam.gameObject.SetActive(false);
        tutorialUI.SetActive(false);
        gameplayCam.gameObject.SetActive(true);

        logSpawner.StartTimer(); 
    }
}
