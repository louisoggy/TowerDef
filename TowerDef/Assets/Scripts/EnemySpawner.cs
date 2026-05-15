using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] waypoints;

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float spawnInterval;
        public GameObject enemyPrefab;
    }

    public Wave[] waves;
    public float timeBetweenWaves = 5f;

    private int currentWave = 0;
    private int enemiesSpawned = 0;
    private float spawnTimer = 0f;
    private float waveDelayTimer = 0f;
    private bool waitingForNextWave = false;
    public bool allWavesComplete = false;

    public static EnemySpawner Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // start first wave immediately
        waitingForNextWave = false;
    }

    void Update()
    {
        if (allWavesComplete) return;

        // Waiting between waves
        if (waitingForNextWave)
        {
            waveDelayTimer += Time.deltaTime;
            if (waveDelayTimer >= timeBetweenWaves)
            {
                waitingForNextWave = false;
                waveDelayTimer = 0f;
                enemiesSpawned = 0;
                Debug.Log("Wave " + (currentWave + 1) + " starting!");
                GameManager.Instance.currentWave = currentWave + 1;
                GameManager.Instance.UpdateUI();
            }
            return;
        }

        // spawning enemies in current wave
        Wave wave = waves[currentWave];
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= wave.spawnInterval && enemiesSpawned < wave.enemyCount)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }

        // Check if wave is done
        if (enemiesSpawned >= wave.enemyCount)
        {
            currentWave++;
            if (currentWave >= waves.Length)
            {
                allWavesComplete = true;
                Debug.Log("All waves spawned!");
                return;
            }
            waitingForNextWave = true;
            Debug.Log("Wave complete! Next wave in " + timeBetweenWaves + " seconds.");
        }
    }

    void SpawnEnemy()
    {
        Wave wave = waves[currentWave];
        GameObject prefabToSpawn = wave.enemyPrefab != null ? wave.enemyPrefab : enemyPrefab;
        GameObject enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        if (movement != null)
            movement.waypoints = waypoints;
        enemiesSpawned++;
        Debug.Log("Spawned enemy " + enemiesSpawned);
    }

}