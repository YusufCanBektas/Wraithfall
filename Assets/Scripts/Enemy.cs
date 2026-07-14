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
    }
}