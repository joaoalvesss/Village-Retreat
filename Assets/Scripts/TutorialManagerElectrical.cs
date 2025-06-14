using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerElectrical : MonoBehaviour
{
    public Camera tutorialCam;
    public Camera gameplayCam;
    public GameObject tutorialUI;
    public GameObject gameplayUI;
    public SpeechBalloon player1Balloon;
    public SpeechBalloon player2Balloon;
    public GameObject continuePrompt;

    public Image image1;
    public Image image2;

    private int step = 0;

    void Start()
    {
        tutorialCam.gameObject.SetActive(true);
        tutorialUI.SetActive(true);
        gameplayCam.gameObject.SetActive(false);
        gameplayUI.SetActive(false);
        continuePrompt.SetActive(false);
        player1Balloon.Hide(image1);
        player2Balloon.Hide(image1);
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
        if (step % 2 != 0)
        {
            player2Balloon.Hide(image1);
        } else
        {
            player1Balloon.Hide(image1);
        }
        continuePrompt.SetActive(true);


        switch (step)
        {
            case 1:
                player1Balloon.Show("Welcome to the Eletrical!", image1);
                break;
            case 2:
                player2Balloon.Show("Rotate the cables to guide the power to the house!", image2);
                break;
            case 3:
                player1Balloon.Show("If the cable is yellow, it has power!", image1);
                break;
            case 4:
                player2Balloon.Show("You must connect all cables to either another cable, a generator or the house!", image2);
                break; 
            case 5:
                player1Balloon.Show("All cables must be powered to win the game!", image1);
                break;           
            case 6:
                player2Balloon.Show("But you will have to work together!", image2);
                break;
            case 7:
                player1Balloon.Show("Each player only controls half of the wall!", image1);
                break;
            case 8:
                player2Balloon.Show("Work together and beat the timer to get electricity to your new house!", image1);
                break;
            case 9:
                player1Balloon.Show("Good luck!", image1);
                break;
            case 10:
                EndTutorial();
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
