using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    public float jumpHeight = 1.5f;
    public float speed = 5f;
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
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        Vector3 worldMove = transform.TransformDirection(moveDirection);
        controller.Move(worldMove * speed * Time.deltaTime);

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
