using UnityEngine;

public class Brush : MonoBehaviour
{
    public Color currentColor = Color.clear;

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (other.TryGetComponent(out WallTile tile))
            {
                tile.Paint(currentColor);
            }

            if (other.TryGetComponent(out PaintSource source))
            {
                currentColor = source.paintColor;
            }
        }
    }
}
