using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    public float jumpHeight = 1.5f;
    public float speed = 15f;
    public float gravity = -9.8f;

    private PlayerAnimationController animController;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animController = GetComponent<PlayerAnimationController>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
        animController.SetGrounded(isGrounded);
    }

    public void ProcessMove(Vector2 input)
    {
        // Movimento para frente e para trás
        Vector3 moveDirection = new Vector3(0, 0, input.y);
        Vector3 worldMove = transform.TransformDirection(moveDirection);
        controller.Move(worldMove * speed * Time.deltaTime);

        // Roda o jogador com o eixo X (A/D)
        transform.Rotate(Vector3.up * input.x * speed * 15f * Time.deltaTime); // A velocidade pode ser ajustada

        // Animações e gravidade (como já tinhas)
        bool isWalking = moveDirection.magnitude > 0.1f;
        animController.SetWalking(isWalking);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }


    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            animController.TriggerJump();
        }
    }
}
