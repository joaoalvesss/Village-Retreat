using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Generator[] generators;
    public Home home;
    public int maxEnergyDepth = 5;

    public void RefreshEnergy()
    {
        foreach (Tile tile in FindObjectsByType<Tile>(FindObjectsSortMode.None))
        {
            tile.SetPowered(false, 0);
        }

        foreach (Generator gen in generators)
        {
            gen.Activate(maxEnergyDepth);
        }

        home.Check();
    }

    private void Start()
    {
        RefreshEnergy();
    }
}