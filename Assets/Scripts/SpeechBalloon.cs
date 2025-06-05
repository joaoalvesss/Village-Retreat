using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeechBalloon : MonoBehaviour
{
    public TextMeshProUGUI speechText;
    public Image speechImage; 

    public void Show(string message, float duration, Sprite optionalImage = null)
    {
        speechText.text = message;

        if (optionalImage != null)
        {
            speechImage.sprite = optionalImage;
            speechImage.gameObject.SetActive(true);
        }
        else
        {
            speechImage.gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
        Invoke(nameof(Hide), duration);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
