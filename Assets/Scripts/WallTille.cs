using UnityEngine;
using FMODUnity;

public class WallTile : MonoBehaviour
{
    public Color targetColor;
    public Color currentColor;
    [HideInInspector] public int targetColorIndex = -1;
    [HideInInspector] public int currentColorIndex = -1;


    private Renderer rend;
    public EventReference paintSound;

    void Start()
    {
        rend = GetComponent<Renderer>();
        UpdateVisual();
    }

    public void Paint(Color color, int colorIndex)
    {
        currentColor = color;
        currentColorIndex = colorIndex;
        UpdateVisual();

        var instance = RuntimeManager.CreateInstance(paintSound);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
        instance.setVolume(0.3f);
        instance.start();
        instance.release();
    }

    public void Clean()
    {
        currentColor = Color.white;
        currentColorIndex = -1;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (rend != null)
            rend.material.color = currentColor;
    }

    public bool IsCorrect()
    {
        if (currentColorIndex != targetColorIndex)
        {
            Debug.Log($"Color index mismatch on tile {name}: targetIndex={targetColorIndex}, currentIndex={currentColorIndex}");
            return false;
        }
        return true;
    }

}
