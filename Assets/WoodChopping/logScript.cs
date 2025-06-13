using UnityEngine;
using System.Collections.Generic;

public class LogController : MonoBehaviour
{
    public float leftSideL;
    public float leftSideR;
    public float rightSideL;
    public float rightSideR;
    public GameObject cutMarkPrefabL; // visual indicator prefab
    public GameObject cutMarkPrefabR; // visual indicator prefab
    public float cutTolerance = 0.5f;
    public LogSpawner spawner;
    public GameObject cutPrefab;

    private List<float> leftCutPoints = new List<float>();
    private List<float> rightCutPoints = new List<float>();

    void Start()
    {
        GenerateCutPoints();
    }

    void GenerateCutPoints()
    {
        // Clear previous
        foreach (Transform child in transform)
        {
            if (child.CompareTag("CutMark"))
                Destroy(child.gameObject);
        }

        leftCutPoints.Clear();
        rightCutPoints.Clear();

        int leftCount = Random.Range(1, 4);
        int rightCount = Random.Range(1, 4);

        float minDistance = 1f;

        // Generate Left Cut Points
        int attempts = 0;
        while (leftCutPoints.Count < leftCount && attempts < 100)
        {
            float x = Random.Range(leftSideL, leftSideR);
            bool tooClose = false;

            foreach (float existing in leftCutPoints)
            {
                if (Mathf.Abs(x - existing) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                leftCutPoints.Add(x);
                SpawnMarkL(x);
            }

            attempts++;
        }

        // Generate Right Cut Points
        attempts = 0;
        while (rightCutPoints.Count < rightCount && attempts < 100)
        {
            float x = Random.Range(rightSideL, rightSideR);
            bool tooClose = false;

            foreach (float existing in rightCutPoints)
            {
                if (Mathf.Abs(x - existing) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                rightCutPoints.Add(x);
                SpawnMarkR(x);
            }

            attempts++;
        }
    }


    void SpawnMarkL(float x)
    {
        Quaternion rotation = Quaternion.Euler(90f, 180f, 0f);
        GameObject mark = Instantiate(cutMarkPrefabL, new Vector3(x, 0f, -0.03f), rotation, transform);
        mark.tag = "CutMark";
        mark.transform.localScale = new Vector3(0.045f, 1f, 0.4f);
    }

    void SpawnMarkR(float x)
    {
        Quaternion rotation = Quaternion.Euler(90f, 180f, 0f);
        GameObject mark = Instantiate(cutMarkPrefabR, new Vector3(x, 0f, -0.03f), rotation, transform);
        mark.tag = "CutMark";
        mark.transform.localScale = new Vector3(0.045f, 1f, 0.4f);
    }

    public void CutAtPosition(float x, int playerId)
    {
        Quaternion rotation = Quaternion.Euler(90f, 180f, 0f);
        Debug.Log($"Player {playerId} is trying to cut a log!");
        List<float> side = playerId == 1 ? leftCutPoints : rightCutPoints;

        for (int i = 0; i < side.Count; i++)
        {
            if (Mathf.Abs(x - side[i]) < cutTolerance)
            {
                Debug.Log($"Player {playerId} cut a log!");

                GameObject cutMade = Instantiate(cutPrefab, new Vector3(side[i], -2f, -0.015f), rotation, transform);
                cutMade.transform.localScale = new Vector3(0.045f, 1f, 0.95f);

                side.RemoveAt(i);
                CheckFinished();

                return;
            }
        }

        Debug.Log($"Player {playerId} missed the cut!");
        GameObject cutMade2 = Instantiate(cutPrefab, new Vector3(x, -2f, -0.015f), rotation, transform);
        cutMade2.transform.localScale = new Vector3(0.045f, 1f, 0.95f);
        if (spawner != null)
        {
            Invoke(nameof(NotifySpawner), 0.5f);
        }
    }

    void CheckFinished()
    {
        if (leftCutPoints.Count == 0 && rightCutPoints.Count == 0)
        {
            Debug.Log("Log fully chopped!");
            // You could start a fall animation or spawn the next log here
            if (spawner != null)
            {
                Invoke(nameof(NotifySpawner), 0.5f); // wait a bit before replacing
            }
        }
    }

    void NotifySpawner()
    {
        spawner.SpawnNewLog();
        Destroy(gameObject);
    }
}
