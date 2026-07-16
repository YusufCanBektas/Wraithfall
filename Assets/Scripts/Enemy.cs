using UnityEngine;

namespace schmup
{
    public class Enemy : Damageable
    {
        [Header("Bewegung")] [SerializeField] float moveSpeed = 3f;
        [SerializeField] bool useSineWave = false;
        [SerializeField] float sineAmplitude = 1f;
        [SerializeField] float sineFrequency = 1f;

        [Header("Schießen")] [SerializeField] GameObject bulletPrefab;
        [SerializeField] Transform firePoint;
        [SerializeField] float fireRate = 2f;

        [Header("Despawn")] [SerializeField] float despawnOffsetX = 15f;

        [Header("Item-Drop")]
        [SerializeField] GameObject[] itemPrefabs; // z.B. Points, ExtraLife, Shield, WeaponUpgrade
        [SerializeField] float dropChance = 0.5f;
        bool itemDropsEnabled = true; // wird vom WaveSpawner gesetzt (ab welcher Welle Drops erlaubt sind)

        [Header("Punkte")]
        [SerializeField] int killScoreValue = 50;

        float startY;
        float nextFireTime;

        protected override void Start()
        {
            base.Start();
            startY = transform.position.y;
            nextFireTime = Time.time + Random.Range(0.5f, fireRate);
        }

        void Update() {
            // Bewegung nach links
            transform.position += Vector3.left * (moveSpeed * Time.deltaTime);

            // Sinuswelle
            if (useSineWave)
            {
                float newY = startY + Mathf.Sin(Time.time * sineFrequency) * sineAmplitude;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }

            // Nur schießen wenn im Sichtbereich der Kamera
            if (transform.position.x < Camera.main.transform.position.x + 12f)
            {
                if (Time.time >= nextFireTime && bulletPrefab != null && firePoint != null)
                {
                    nextFireTime = Time.time + fireRate;
                    Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                }
            }

            // Aus dem Bild → zerstören (kameraabhängig)
            if (transform.position.x < Camera.main.transform.position.x - despawnOffsetX)
            {
                Destroy(gameObject);
            }
        }

        // Wird vom WaveSpawner direkt nach dem Instantiate aufgerufen
        public void SetItemDropsEnabled(bool enabled)
        {
            itemDropsEnabled = enabled;
        }

        protected override void Die()
        {
            ScoreManager.Instance?.AddPoints(killScoreValue);
            TryDropItem();
            base.Die();
        }

        void TryDropItem()
        {
            if (!itemDropsEnabled) return;
            if (itemPrefabs == null || itemPrefabs.Length == 0) return;
            if (Random.value > dropChance) return;

            GameObject chosenItem = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            Instantiate(chosenItem, transform.position, Quaternion.identity);
        }
    }
}