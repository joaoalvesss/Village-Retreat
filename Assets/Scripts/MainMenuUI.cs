using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject mainMenuPanel; 

    public void StartGame()
    {
        SceneManager.LoadScene("Island");
    }

    public void OpenCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelectScene");
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
