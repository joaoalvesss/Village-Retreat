using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    public GameObject logPrefab;
    public Transform spawnPoint;

    public GameObject currentLog;
    public LogController currentLogController => currentLog?.GetComponent<LogController>();

    void Start()
    {
        SpawnNewLog();
    }

    public void SpawnNewLog()
    {
        if (currentLog != null)
        {
            Destroy(currentLog);
        }

        Quaternion rotation = Quaternion.Euler(90f, 180f, 0f);
        currentLog = Instantiate(logPrefab, spawnPoint.position, rotation);
        currentLog.GetComponent<LogController>().spawner = this; // Let log notify us when it's done
    }
}
