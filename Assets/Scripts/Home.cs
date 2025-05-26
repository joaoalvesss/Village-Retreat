using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public Direction outputDirection;

    public void Check()
    {
        Tile target = GetAdjacentTile(outputDirection);
        if (target == null) return;

        Direction reverse = GetOppositeDirection(outputDirection);
        if (target.HasOpenSide(reverse))
        {
            bool test = true;
            foreach (Tile tile in FindObjectsByType<Tile>(FindObjectsSortMode.None))
            {
                if (!tile.IsPowered)
                    test = false;
            }
            if (test)
                SceneManager.LoadScene("Island", LoadSceneMode.Single); //Load da cena principal se ganhar
        }
    }

    private Tile GetAdjacentTile(Direction dir)
    {
        Vector2 offset = DirectionToVector(dir);
        Vector3 targetPosition = transform.position + new Vector3(-offset.x, offset.y, 0);
        Collider[] hits = Physics.OverlapSphere(targetPosition, 0.1f);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Tile>() != null) return hit.GetComponent<Tile>();
        }
        return null;
    }

    private Vector2 DirectionToVector(Direction dir)
    {
        return dir switch
        {
            Direction.Up => Vector2.up,
            Direction.Down => Vector2.down,
            Direction.Left => Vector2.left,
            Direction.Right => Vector2.right,
            _ => Vector2.zero
        };
    }

    private Direction GetOppositeDirection(Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            _ => dir
        };
    }
}
