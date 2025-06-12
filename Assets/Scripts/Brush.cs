using UnityEngine;

public class Brush : MonoBehaviour
{
    public Color currentColor = Color.clear;
    private WallTile overlappingTile;
    private PaintSource overlappingSource;
    public UnityEngine.UI.Image brushColorUI;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        UpdateBrushColorVisual();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (overlappingTile != null)
            {
                overlappingTile.Paint(currentColor);
            }

            if (overlappingSource != null)
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
            brushColorUI.color = new Color(255, 255, 255, 255); // fully transparent
        }
        else
        {
            Color uiColor = currentColor;
            uiColor.a = 1f;
            brushColorUI.color = uiColor;
        }
    }
}
