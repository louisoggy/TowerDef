using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] waypoints;
    public float spawnInterval = 2f;
    public int enemiesPerWave = 5;

    private int enemiesSpawned = 0;
    private float spawnTimer = 0f;
    private bool waveActive = true;

    void Update()
    {
        if (!waveActive) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && enemiesSpawned < enemiesPerWave)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }

        if (enemiesSpawned >= enemiesPerWave)
        {
            waveActive = false;
            Debug.Log("Wave complete!");
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        if (movement != null)
            movement.waypoints = waypoints;

        enemiesSpawned++;
        Debug.Log("Enemy " + enemiesSpawned + " spawned!");
    }
}