using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    public float jumpHeight = 1.5f;
    public float speed = 15f;
    public float gravity = -9.8f;
    public float groundCheckDistance = 0.5f; // Dist�ncia para verificar o ch�o � frente

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
        // Movimento para frente e para tr�s
        Vector3 moveDirection = new Vector3(0, 0, input.y);
        Vector3 worldMove = transform.TransformDirection(moveDirection);
        Vector3 moveAmount = worldMove * speed * Time.deltaTime;

        // Verifica se h� ch�o na posi��o futura antes de mover
        bool canMove = CheckGroundAhead(moveAmount);

        // Roda o jogador com o eixo X (A/D) - a rota��o n�o precisa de verifica��o
        transform.Rotate(Vector3.up * input.x * speed * 15f * Time.deltaTime);

        // Anima��es e movimento
        bool isWalking = moveDirection.magnitude > 0.1f && canMove;
        animController.SetWalking(isWalking);

        // Aplica movimento apenas se houver ch�o � frente
        if (canMove)
        {
            controller.Move(moveAmount);
        }

        // Gravidade
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private bool CheckGroundAhead(Vector3 moveAmount)
    {
        // Se n�o est� se movendo, considera que pode mover (para ficar parado)
        if (moveAmount.magnitude < 0.001f) return true;

        // Calcula posi��o futura
        Vector3 futurePosition = transform.position + moveAmount;

        // Lan�a raycast para baixo a partir da posi��o futura
        if (Physics.Raycast(futurePosition + Vector3.up * 0.1f, Vector3.down, out RaycastHit hit, groundCheckDistance + 0.1f))
        {
            return true; // Tem ch�o � frente
        }

        return false; // N�o tem ch�o � frente
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