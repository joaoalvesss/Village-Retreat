using UnityEngine;

public class Brush : MonoBehaviour
{
    public Color currentColor = Color.clear;
    public int currentColorIndex = -1;

    private WallTile overlappingTile;
    private PaintSource overlappingSource;
    public UnityEngine.UI.Image brushColorUI;
    private Renderer rend;
    public KeyCode paintKey = KeyCode.Space;
    public PlayerOwner playerOwner;
    private bool hasPaint = false;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Assign default starting color based on player owner
        Color[] palette = FindFirstObjectByType<PatternManager>().palette.colors;

        if (playerOwner == PlayerOwner.Player1 && palette.Length > 0)
        {
            currentColor = palette[0];
            currentColorIndex = 0;
        }
        else if (playerOwner == PlayerOwner.Player2 && palette.Length > 1)
        {
            currentColor = palette[1];
            currentColorIndex = 1;
        }

        UpdateBrushColorVisual();
    }

    void Update()
    {
        if (Input.GetKeyDown(paintKey))
        {
            // Only update color if overlapping a source
            if (overlappingSource != null && overlappingSource.owner == playerOwner)
            {
                currentColor = overlappingSource.paintColor;
                currentColorIndex = overlappingSource.colorIndex;
                UpdateBrushColorVisual();
                Debug.Log($"Picked up color: index={currentColorIndex}, color={currentColor}");
            }

            // Only paint if brush has valid color index
            if (overlappingTile != null && currentColorIndex >= 0)
            {
                overlappingTile.Paint(currentColor, currentColorIndex);

                if (FindFirstObjectByType<PatternManager>()?.IsPatternMatched() == true)
                {
                    FindFirstObjectByType<GameManagerPainting>()?.EndGame("YOU WON!");
                }
            }
            else if (overlappingTile != null)
            {
                Debug.LogWarning($"Cannot paint: brush has no valid color index. currentIndex={currentColorIndex}");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WallTile tile))
            overlappingTile = tile;

        if (other.TryGetComponent(out PaintSource source))
            overlappingSource = source;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out WallTile tile) && overlappingTile == tile)
            overlappingTile = null;

        if (other.TryGetComponent(out PaintSource source) && overlappingSource == source)
            overlappingSource = null;
    }

    void UpdateBrushColorVisual()
    {
        if (rend != null)
            rend.material.color = currentColor;

        if (currentColor == Color.clear)
        {
            brushColorUI.color = new Color(255, 255, 255, 255);
        }
        else
        {
            Color uiColor = currentColor;
            uiColor.a = 1f;
            brushColorUI.color = uiColor;
        }
    }
    
    int FindColorIndex(Color color)
    {
        Color[] palette = FindFirstObjectByType<PatternManager>().palette.colors;
        for (int i = 0; i < palette.Length; i++)
        {
            if (AreColorsClose(palette[i], color, 0.01f))
                return i;
        }
        return -1;
    }

    bool AreColorsClose(Color a, Color b, float tolerance)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
            Mathf.Abs(a.g - b.g) < tolerance &&
            Mathf.Abs(a.b - b.b) < tolerance;
    }

}
