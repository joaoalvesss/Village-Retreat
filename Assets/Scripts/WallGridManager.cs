using UnityEngine;

public class WallGridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public int width = 10;
    public int height = 10;
    public float spacing = 1.1f;

    void Start()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 pos = new(x * spacing, y * spacing, 0);
                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
