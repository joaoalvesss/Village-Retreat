using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class MainMenuUI : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject mainMenuPanel; 
    public string skipSound = "event:/UI/Skip";
    public string contSound = "event:/UI/UI_click_menu_hover";

    public void StartGame()
    {
        RuntimeManager.PlayOneShot(skipSound, transform.position);
        SceneManager.LoadScene("Island");
    }

    public void OpenOptions()
    {
        RuntimeManager.PlayOneShot(contSound, transform.position);
        optionsPanel.SetActive(true);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false); 
    }

    public void CloseOptions()
    {
        RuntimeManager.PlayOneShot(contSound, transform.position);
        optionsPanel.SetActive(false);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        RuntimeManager.PlayOneShot(skipSound, transform.position);
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
