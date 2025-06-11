using UnityEngine;

public class WallTile : MonoBehaviour
{
    public Color targetColor;
    public Color currentColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        UpdateVisual();
    }

    public void Paint(Color color)
    {
        currentColor = color;
        UpdateVisual();
    }

    public void Clean()
    {
        currentColor = Color.white;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (rend != null)
            rend.material.color = currentColor;
    }

    public bool IsCorrect()
    {
        return currentColor == targetColor;
    }
}
