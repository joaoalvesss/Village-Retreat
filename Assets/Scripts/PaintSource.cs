using UnityEngine;

public enum PlayerOwner { Player1, Player2 }

public class PaintSource : MonoBehaviour
{
    public Color paintColor;
    public int colorIndex;
    public PlayerOwner owner;

    void Start()
    {
        GetComponent<Renderer>().material.color = paintColor;
    }
}

