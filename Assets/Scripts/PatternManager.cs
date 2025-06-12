using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public Color[] possibleColors;
    public WallTile[] tiles;

    void Start()
    {
        GeneratePattern();
    }

    public void GeneratePattern()
    {
        foreach (WallTile tile in tiles)
        {
            tile.targetColor = possibleColors[Random.Range(0, possibleColors.Length)];
        }
    }

    public bool IsPatternMatched()
    {
        foreach (WallTile tile in tiles)
        {
            if (!tile.IsCorrect())
                return false;
        }
        return true;
    }

    public void SetTiles(WallTile[] newTiles)
    {
        tiles = newTiles;
        GeneratePattern();
    }

    public void ResetPattern()
    {
        foreach (WallTile tile in tiles)
        {
            tile.Clean();
        }
        GeneratePattern();
    }
}
