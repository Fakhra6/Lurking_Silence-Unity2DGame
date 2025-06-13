using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    public static PlayerSpawnManager Instance;

    private string lastSpawnID;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSpawnPoint(string spawnID)
    {
        lastSpawnID = spawnID;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (string.IsNullOrEmpty(lastSpawnID)) return;

        SpawnPoint spawn = FindSpawnPoint(lastSpawnID);
        if (spawn != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = spawn.transform.position;
            }
        }
    }

    private SpawnPoint FindSpawnPoint(string spawnID)
    {
        SpawnPoint[] spawnPoints = Object.FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
        foreach (var point in spawnPoints)
        {
            if (point.spawnID == spawnID)
            {
                return point;
            }
        }

        Debug.LogWarning("No spawn point found with ID: " + spawnID);
        return null;
    }


}
