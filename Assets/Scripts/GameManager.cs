using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pot[] pots;

    void Update()
    {
        if (AllPotsOnTargets())
        {
            // Debug.Log("Puzzle Complete!");
            // Add effects, transitions, etc.
        }
    }

    bool AllPotsOnTargets()
    {
        foreach (Pot pot in pots)
        {
            if (!pot.IsOnTarget()) return false;
        }
        return true;
    }

    
}
