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
        return AreColorsSimilar(currentColor, targetColor, 0.01f);
    }

    private bool AreColorsSimilar(Color a, Color b, float tolerance)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
            Mathf.Abs(a.g - b.g) < tolerance &&
            Mathf.Abs(a.b - b.b) < tolerance;
    }

}
