using UnityEngine;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}

public class Tile : MonoBehaviour
{
    public Direction[] openSides;
    public bool IsPowered { get; private set; }

    public void SetPowered(bool state)
    {
        if (IsPowered == state) return;

        IsPowered = state;
        UpdateVisual();

        if (IsPowered)
            PropagateEnergy();
    }

    private void PropagateEnergy()
    {
        foreach (Direction dir in openSides)
        {
            Tile neighbor = GetAdjacentTile(dir);
            if (neighbor == null) continue;

            Direction reverse = GetOppositeDirection(dir);
            if (neighbor.HasOpenSide(reverse))
            {
                neighbor.SetPowered(true);
            }
        }
    }

    public bool HasOpenSide(Direction dir)
    {
        foreach (var side in openSides)
            if (side == dir) return true;

        return false;
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

    private Direction RotateDirectionClockwise(Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => dir,
        };
    }

    public void RotateClockwise()
    {
        transform.Rotate(0, 0, 90f);

        for (int i = 0; i < openSides.Length; i++)
        {
            openSides[i] = RotateDirectionClockwise(openSides[i]);
        }
    }

    private void UpdateVisual()
    {  
        Material Powered = Resources.Load("Materials/Powered", typeof(Material)) as Material;
        Material Wire = Resources.Load("Materials/Wire", typeof(Material)) as Material;
        foreach (Transform child in transform)
        {
            if (child.name == "Cube")
                continue;  // skip this child

            GameObject childGO = child.gameObject;
            childGO.GetComponent<MeshRenderer>().material = IsPowered ? Powered : Wire;
        }
    }
}
