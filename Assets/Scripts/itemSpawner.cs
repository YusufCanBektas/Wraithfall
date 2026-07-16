using UnityEngine;

namespace schmup
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] itemPrefabs;
        [SerializeField] Transform spawnPoint;

        [Header("Timing")]
        [SerializeField] float minSpawnInterval = 4f;
        [SerializeField] float maxSpawnInterval = 6f;

        [Header("Spawn Position Variation")]
        [SerializeField] float minSpawnY = -3f;
        [SerializeField] float maxSpawnY = 3f;

        [Header("Freigabe")]
        [SerializeField] int startFromWave = 1; // ab welcher Welle diese Items aktiv werden

        float nextSpawnTime;
        WaveSpawner waveSpawner;

        void Start()
        {
            waveSpawner = FindFirstObjectByType<WaveSpawner>();
            ScheduleNextSpawn();
        }

        void Update()
        {
            if (Time.time < nextSpawnTime) return;
            if (waveSpawner != null && waveSpawner.GetCurrentWaveNumber() < startFromWave) return;

            SpawnItem();
            ScheduleNextSpawn();
        }

        void ScheduleNextSpawn()
        {
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        void SpawnItem()
        {
            if (itemPrefabs == null || itemPrefabs.Length == 0 || spawnPoint == null) return;

            float randomY = Random.Range(minSpawnY, maxSpawnY);
            Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y + randomY, spawnPoint.position.z);

            GameObject chosenItem = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            Instantiate(chosenItem, spawnPos, Quaternion.identity);
        }
    }
}