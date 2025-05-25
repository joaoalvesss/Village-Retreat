using UnityEngine;

public class Pot : MonoBehaviour
{
    public Vector2Int gridPos;

    private GridManager gridManager;

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
    }

    public bool IsOnTarget()
    {
        return gridManager.GetTileType(gridPos) == TileType.Target;
    }

    void Update()
    {
        gridPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }
}
