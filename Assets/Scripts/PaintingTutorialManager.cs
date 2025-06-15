using UnityEngine;
using UnityEngine.UI;

public class PaintingTutorialManager : MonoBehaviour
{
     public Camera tutorialCam;
     public Camera gameplayCam;
     public GameObject tutorialUI;
     public GameObject gameplayUI;
     public GameObject gameplayUI2;
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
          gameplayUI2.SetActive(false);
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
          player2Balloon.Hide(image2);
          continuePrompt.SetActive(true);

          step++;
          switch (step)
          {
               case 1:
                    player1Balloon.Show("Welcome to the Painting Room!", image1);
                    break;
               case 2:
                    player2Balloon.Show("Your goal is to match the wall with the pattern on the right!", image2);
                    break;
               case 3:
                    player1Balloon.Show("Move around using WASD or Arrows.", image1);
                    break;
               case 4:
                    player2Balloon.Show("Press SPACE or ENTER to pick colors from buckets.", image2);
                    break;
               case 5:
                    player1Balloon.Show("Then go to the wall and press your key again to paint!", image1);
                    break;
               case 6:
                    player2Balloon.Show("Work together to complete the pattern before time runs out!", image2);
                    break;
               case 7:
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
          gameplayUI2.SetActive(true);

          GameManagerPainting gm = FindFirstObjectByType<GameManagerPainting>();
          if (gm != null)
          {
               gm.enabled = true;           
               gm.StartTimer();            
          }
    }
}
