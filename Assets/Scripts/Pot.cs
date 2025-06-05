using UnityEngine;

public enum PotType { LightGreen, DarkGreen, White }

public class Pot : MonoBehaviour
{

    public PotType potType;
    public Vector2Int gridPos;

    private GridManager gridManager;

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
    }

    public void CheckForDestruction()
{
    if (gridManager.GetTileType(gridPos) == TileType.Water)
    {
        GameManagerZenGarden.Instance.ShowPotDestroyedMessage(); 
        gridManager.potPositions.Remove(gridPos);
        Destroy(gameObject);
    }
}

    public bool IsOnTarget()
    {
        return gridManager.GetTileType(gridPos) == TileType.Target;
    }

    void Update()
    {
        gridPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        CheckForDestruction();
    }
}
