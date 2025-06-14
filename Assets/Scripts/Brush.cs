using UnityEngine;

public class Brush : MonoBehaviour
{
    public Color currentColor = Color.clear;
    private WallTile overlappingTile;
    private PaintSource overlappingSource;
    public UnityEngine.UI.Image brushColorUI;
    private Renderer rend;
    public KeyCode paintKey = KeyCode.Space;
    public PlayerOwner playerOwner;

    void Start()
    {
        rend = GetComponent<Renderer>();
        UpdateBrushColorVisual();
    }

    void Update()
    {
        if (Input.GetKeyDown(paintKey))
        {
            if (overlappingTile != null)
            {
                overlappingTile.Paint(currentColor);
                if (FindFirstObjectByType<PatternManager>()?.IsPatternMatched() == true)
                {
                    FindFirstObjectByType<GameManagerPainting>()?.EndGame("YOU WON!");
                }

            }

            if (overlappingSource != null && overlappingSource.owner == playerOwner)
            {
                currentColor = overlappingSource.paintColor;
                Debug.Log("Picked up color: " + currentColor);
                UpdateBrushColorVisual();
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
}
