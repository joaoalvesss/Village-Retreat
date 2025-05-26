using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    public TextMeshProUGUI player1PromptText;
    public TextMeshProUGUI player2PromptText;

    private bool player1Nearby = false;
    private bool player2Nearby = false;

    private bool player1Ready = false;
    private bool player2Ready = false;

    void Start()
    {
        player1PromptText.gameObject.SetActive(false);
        player2PromptText.gameObject.SetActive(false);
    }

    void Update()
    {
        // PLAYER 1 INTERAÇÃO
        if (player1Nearby)
        {
            if (!player1Ready)
            {
                player1PromptText.text = "Press ALT to play";

                if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
                {
                    player1Ready = true;
                    player1PromptText.text = "Esperando o segundo jogador...\n(Press ESC para sair)";
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    player1Ready = false;
                    player1PromptText.text = "Press ALT to play";
                }
            }
        }

        // PLAYER 2 INTERAÇÃO
        if (player2Nearby)
        {
            if (!player2Ready)
            {
                player2PromptText.text = "Press ENTER to play";

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    player2Ready = true;
                    player2PromptText.text = "Esperando o segundo jogador...\n(Press ESC para sair)";
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    player2Ready = false;
                    player2PromptText.text = "Press ENTER to play";
                }
            }
        }

        // INICIAR JOGO SE OS DOIS ESTÃO PRONTOS
        if (player1Ready && player2Ready)
        {
            player1PromptText.text = "A iniciar jogo...";
            player2PromptText.text = "A iniciar jogo...";
            Invoke("StartMiniGame", 1.5f);
        }
    }

    void StartMiniGame()
    {
        SceneManager.LoadScene("MiniGameScene");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            player1Nearby = true;
            player1PromptText.gameObject.SetActive(true);
        }

        if (other.CompareTag("Player2"))
        {
            player2Nearby = true;
            player2PromptText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            player1Nearby = false;
            player1PromptText.gameObject.SetActive(false);
            player1Ready = false;
        }

        if (other.CompareTag("Player2"))
        {
            player2Nearby = false;
            player2PromptText.gameObject.SetActive(false);
            player2Ready = false;
        }
    }
}
