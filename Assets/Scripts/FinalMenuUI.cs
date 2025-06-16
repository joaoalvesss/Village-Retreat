using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMenuUI : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject mainMenuPanel; 

    public void StartGame()
    {
        GlobalVariables.Instance.bush = 0;
        GlobalVariables.Instance.wood = 0;
        GlobalVariables.Instance.ink = 0;
        GlobalVariables.Instance.light = 0;
        SceneManager.LoadScene("Island");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false); 
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
