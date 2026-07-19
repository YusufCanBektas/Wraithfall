using UnityEngine;

namespace schmup
{
    public class BossController : Damageable
    {
        [Header("Position")]
        [SerializeField] Vector3 offsetFromCamera = new Vector3(6f, 0f, 0f);

        [Header("Ausrichtung (per Ausprobieren in der Game-View gefunden)")]
        [SerializeField] Vector3 modelRotation = new Vector3(-90f, 0f, 0f); // TODO: hier deinen gefundenen Wert eintragen

        [Header("Patrouille (nur vertikal)")]
        [SerializeField] float patrolSpeed = 2f;
        [SerializeField] float patrolRangeY = 3f;

        [Header("Schießen")]
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] Transform firePoint;
        [SerializeField] float fireRate = 1.5f;
        [SerializeField] Vector3 firePointLocalOffset = new Vector3(-1f, 0f, 0f);

        [Header("Punkte")]
        [SerializeField] int defeatScoreValue = 1000;

        [Header("Effekte")]
        [SerializeField] GameObject explosionPrefab;
        [SerializeField] float explosionScale = 2.5f; // Boss-Explosion größer als bei normalen Gegnern

        [Header("Level-Ende")]
        [SerializeField] bool isFinalBoss = false; // true beim zweiten Boss in Level 2, löst das eigentliche Spielende aus

        float baseY;
        float startY;
        float nextFireTime;
        int direction = 1;
        HUDController hud;

        protected override void Start()
        {
            base.Start();
            baseY = transform.position.y;
            startY = baseY;
            nextFireTime = Time.time + 1f;
            transform.rotation = Quaternion.Euler(modelRotation);

            hud = FindFirstObjectByType<HUDController>();
            hud?.ShowBossHealthBar(this);
        }

        void Update()
        {
            float targetX = Camera.main.transform.position.x + offsetFromCamera.x;

            baseY += direction * patrolSpeed * Time.deltaTime;
            if (baseY > startY + patrolRangeY)
                direction = -1;
            else if (baseY < startY - patrolRangeY)
                direction = 1;

            transform.position = new Vector3(targetX, baseY, offsetFromCamera.z);

            if (Time.time >= nextFireTime && bulletPrefab != null)
            {
                nextFireTime = Time.time + fireRate;
                // Spawn-Position wird direkt vom Boss-Transform berechnet (fester Offset),
                // statt firePoint.position blind zu übernehmen. Das macht den Spawn unabhängig
                // von einer eventuell schräg-rotierten Parent-Hierarchie.
                Vector3 spawnPos = transform.position + firePointLocalOffset;
                spawnPos.z = -5f;
                Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                Debug.Log($"Boss schießt von Position: {spawnPos}");
            }
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            hud?.UpdateBossHealth(this);
        }

        protected override void Die()
        {
            Debug.Log(isFinalBoss ? "Finaler Boss besiegt! Sieg!" : "Boss besiegt! Weiter zu Level 2.");
            ScoreManager.Instance?.AddPoints(defeatScoreValue);
            SpawnExplosion();
            AudioManager.Instance?.PlayExplosion();
            AudioManager.Instance?.PlayBossDefeated();
            hud?.HideBossHealthBar();

            if (isFinalBoss)
                GameManager.Instance?.OnFinalBossDefeated();
            else
                GameManager.Instance?.OnBossDefeated();

            Destroy(gameObject);
        }

        void SpawnExplosion()
        {
            if (explosionPrefab == null) return;
            GameObject fx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            fx.transform.localScale *= explosionScale;
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log($"BOSS OnTriggerEnter mit: {other.name} (Tag: {other.tag})");
            if (other.CompareTag("PlayerBullet"))
            {
                TakeDamage(1);
                Destroy(other.gameObject);
            }
        }
    }
}