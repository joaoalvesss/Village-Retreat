using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Generator[] generators;
    public Home home;

    public void RefreshEnergy()
    {
        foreach (Tile tile in FindObjectsByType<Tile>(FindObjectsSortMode.None))
        {
            tile.SetPowered(false);
        }

        foreach (Generator gen in generators)
        {
            gen.Activate();
        }

        home.Check();
    }

    private void Start()
    {
        RefreshEnergy();
    }
}
