using UnityEngine;

public class TargetTile : MonoBehaviour
{
    public Vector2Int gridPos;
    private GridManager gridManager;

    public bool isOccupied; // set true if a pot is on this tile

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        gridPos = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.z)
        );
    }

    void Update()
    {
        // Optional: actively check if a pot is on this tile
        isOccupied = gridManager.potPositions.ContainsKey(gridPos);
    }
}
