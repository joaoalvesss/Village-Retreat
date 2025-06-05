using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Camera tutorialCam;
    public Camera gameplayCam;
    public GameObject tutorialUI;
    public GameObject gameplayUI;
    public SpeechBalloon player1Balloon;
    public SpeechBalloon player2Balloon;
    public GameObject continuePrompt;

    private int step = 0;

    void Start()
    {
        tutorialCam.gameObject.SetActive(true);
        tutorialUI.SetActive(true);
        gameplayCam.gameObject.SetActive(false);
        gameplayUI.SetActive(false);
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
        step++;
        switch (step)
        {
            case 1:
                player1Balloon.Show("Welcome to the Zen Garden challenge!", 3f);
                break;
            case 2:
                player2Balloon.Show("Move the pots onto the matching colored targets!", 4f);
                break;
            case 3:
                player1Balloon.Show("But be careful... water will destroy your pots!", 4f);
                break;
            case 4:
                player2Balloon.Show("Get more points by placing pots quickly and correctly!", 4f);
                break;
            case 5:
                continuePrompt.SetActive(true); 
                break;
        }
    }

    public void EndTutorial()
    {
        tutorialCam.gameObject.SetActive(false);
        tutorialUI.SetActive(false);
        gameplayCam.gameObject.SetActive(true);
        gameplayUI.SetActive(true);
    }
}

