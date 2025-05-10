using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }

    public void OpenOptions()
    {
        // You could show an options panel here instead
        Debug.Log("Options menu opened");
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
