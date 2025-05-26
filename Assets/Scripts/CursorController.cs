using UnityEngine;

public class CursorController : MonoBehaviour
{
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public KeyCode rotateKey = KeyCode.E;
    public Vector2 heightLimit;
    public Vector2 widthLimit;

    public float moveCooldown = 0.2f;
    private float lastMoveTime;

    public float gridSize = 1f;

    void Update()
    {
        if (Time.time - lastMoveTime >= moveCooldown)
        {
            float h = Input.GetAxisRaw(horizontalAxis);
            float v = Input.GetAxisRaw(verticalAxis);

            if (h != 0 || v != 0)
            {
                if (transform.position.y + v <= heightLimit.x && transform.position.y + v >= heightLimit.y && transform.position.x - h <= widthLimit.x && transform.position.x - h >= widthLimit.y) {
                    transform.position += new Vector3(-h, v, 0) * gridSize;
                    lastMoveTime = Time.time;
                }
            }
        }

        if (Input.GetKeyDown(rotateKey))
        {
            TryRotateTile();
        }
    }

    void TryRotateTile()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.1f);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Tile"))
            {
                hit.GetComponent<Tile>().RotateClockwise();
                Object.FindAnyObjectByType<GameManager>().RefreshEnergy();
                break;
            }
        }
    }
}
