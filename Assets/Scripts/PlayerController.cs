using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode up, down, left, right;
    public float moveDelay = 0.1f;
    public float verticalStepSize = 1.14f;
    public float horizontalStepSize = 1.205f;

    public Vector2Int currentPos;
    private bool isMoving;

    private GridManager gridManager;

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();

        if (gridManager == null)
            Debug.LogError("GridManager not found!");


        // Convert player's world position to grid position (inverted Z)
        // currentPos = new Vector2Int(
        //     Mathf.RoundToInt(transform.position.x / horizontalStepSize),
        //     Mathf.RoundToInt(transform.position.z / verticalStepSize)
        // );

        currentPos = new Vector2Int(5, 9); // Replace with any walkable grid tile
        transform.position = GridToWorld(currentPos);   

        Debug.Log($"Starting on tile: {gridManager.GetTileType(currentPos)}");
        if (!gridManager.IsInBounds(currentPos))
        {
            Debug.LogError("Player starting position is out of bounds!");
            return;
        }
    }


    void Update()
    {
        if (isMoving) return;

        Vector2Int dir = Vector2Int.zero;

        if (Input.GetKeyDown(up)) dir = Vector2Int.down;
        else if (Input.GetKeyDown(down)) dir = Vector2Int.up;
        else if (Input.GetKeyDown(left)) dir = Vector2Int.right;
        else if (Input.GetKeyDown(right)) dir = Vector2Int.left;

        if (dir != Vector2Int.zero)
        {
            TryMove(dir);
        }
    }

    void TryMove(Vector2Int direction)
    {
        Vector2Int targetPos = currentPos + direction; 
        Debug.Log($"Trying to move to {targetPos}");
        if (!gridManager.IsInBounds(targetPos)) Debug.LogWarning("Target position is out of bounds!");

        // Is pot in the way?
        if (gridManager.potPositions.ContainsKey(targetPos))
        {
            Vector2Int potTargetPos = targetPos + direction;
            if (gridManager.TryMovePot(targetPos, potTargetPos))
            {
                MoveTo(targetPos); // Move player to where pot was
            }
        }
        else if (gridManager.IsTileWalkable(targetPos))
        {
            MoveTo(targetPos);
        }
    }

    void MoveTo(Vector2Int newPos)
    {
        currentPos = newPos;
        transform.position = GridToWorld(newPos); 
        StartCoroutine(MoveCooldown());
    }

    System.Collections.IEnumerator MoveCooldown()
    {
        isMoving = true;
        yield return new WaitForSeconds(moveDelay);
        isMoving = false;
    }

    Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(
            gridPos.x * horizontalStepSize,
            transform.position.y,
            gridPos.y * verticalStepSize
        );
    }
}
