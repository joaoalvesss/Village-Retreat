using UnityEngine;

public class PaintSource : MonoBehaviour
{
    public Color paintColor;

    void Start()
    {
        GetComponent<Renderer>().material.color = paintColor;
    }
}
