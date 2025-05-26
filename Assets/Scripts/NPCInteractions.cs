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

    private GameObject player1InteractingWith = null;
    private GameObject player2InteractingWith = null;


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
                if (player1InteractingWith != null && player1InteractingWith.name == "WoodWorker")
                {
                    player1PromptText.text = "Press ALT to play WoodWorker";
                }else if (player1InteractingWith != null && player1InteractingWith.name == "Bricklayer")
                {
                    player1PromptText.text = "Press ALT to play Bricklayer";
                }else if (player1InteractingWith != null && player1InteractingWith.name == "Painter")
                {
                    player1PromptText.text = "Press ALT to play Painter";
                }else if (player1InteractingWith != null && player1InteractingWith.name == "Gardener")
                {
                    player1PromptText.text = "Press ALT to play Gardener";
                }
                else if (player1InteractingWith != null && player1InteractingWith.name == "Electrician")
                {
                    player1PromptText.text = "Press ALT to play Electrician";
                }

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
                    if (player1InteractingWith != null && player1InteractingWith.name == "WoodWorker")
                    {
                        player1PromptText.text = "Press ALT to play WoodWorker";
                    }
                    else if (player1InteractingWith != null && player1InteractingWith.name == "Bricklayer")
                    {
                        player1PromptText.text = "Press ALT to play Bricklayer";
                    }
                    else if (player1InteractingWith != null && player1InteractingWith.name == "Painter")
                    {
                        player1PromptText.text = "Press ALT to play Painter";
                    }
                    else if (player1InteractingWith != null && player1InteractingWith.name == "Gardener")
                    {
                        player1PromptText.text = "Press ALT to play Gardener";
                    }
                    else if (player1InteractingWith != null && player1InteractingWith.name == "Electrician")
                    {
                        player1PromptText.text = "Press ALT to play Electrician";
                    }
                }
            }
        }

        // PLAYER 2 INTERAÇÃO
        if (player2Nearby)
        {
            if (!player2Ready)
            {
                if (player2InteractingWith != null && player2InteractingWith.name == "WoodWorker")
                {
                    player2PromptText.text = "Press ALT to play WoodWorker";
                }
                else if (player2InteractingWith != null && player2InteractingWith.name == "Bricklayer")
                {
                    player2PromptText.text = "Press ALT to play Bricklayer";
                }
                else if (player2InteractingWith != null && player2InteractingWith.name == "Painter")
                {
                    player2PromptText.text = "Press ALT to play Painter";
                }
                else if (player2InteractingWith != null && player2InteractingWith.name == "Gardener")
                {
                    player2PromptText.text = "Press ALT to play Gardener";
                }
                else if (player2InteractingWith != null && player2InteractingWith.name == "Electrician")
                {
                    player2PromptText.text = "Press ALT to play Electrician";
                }

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
                    if (player2InteractingWith != null && player2InteractingWith.name == "WoodWorker")
                    {
                        player2PromptText.text = "Press ALT to play WoodWorker";
                    }
                    else if (player2InteractingWith != null && player2InteractingWith.name == "Bricklayer")
                    {
                        player2PromptText.text = "Press ALT to play Bricklayer";
                    }
                    else if (player2InteractingWith != null && player2InteractingWith.name == "Painter")
                    {
                        player2PromptText.text = "Press ALT to play Painter";
                    }
                    else if (player2InteractingWith != null && player2InteractingWith.name == "Gardener")
                    {
                        player2PromptText.text = "Press ALT to play Gardener";
                    }
                    else if (player2InteractingWith != null && player2InteractingWith.name == "Electrician")
                    {
                        player2PromptText.text = "Press ALT to play Electrician";
                    }
                }
            }
        }

        // INICIAR JOGO SE OS DOIS ESTÃO PRONTOS
        if (player1Ready && player2Ready && (player1InteractingWith == player2InteractingWith))
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
            player1InteractingWith = gameObject;

        }

        if (other.CompareTag("Player2"))
        {
            player2Nearby = true;
            player2PromptText.gameObject.SetActive(true);
            player2InteractingWith = gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            player1Nearby = false;
            player1PromptText.gameObject.SetActive(false);
            player1Ready = false;
            player1InteractingWith = null;
        }

        if (other.CompareTag("Player2"))
        {
            player2Nearby = false;
            player2PromptText.gameObject.SetActive(false);
            player2Ready = false;
            player2InteractingWith = null;
        }
    }
}
