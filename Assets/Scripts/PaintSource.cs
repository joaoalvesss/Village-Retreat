using UnityEngine;

public enum PlayerOwner { Player1, Player2 }

public class PaintSource : MonoBehaviour
{
    public Color paintColor;
    public PlayerOwner owner;

    void Start()
    {
        GetComponent<Renderer>().material.color = paintColor;
    }
}
