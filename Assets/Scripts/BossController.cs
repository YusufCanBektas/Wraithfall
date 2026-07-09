using UnityEngine;

namespace schmup
{
    public class BossController : MonoBehaviour
    {
        [Header("Position")]
        [SerializeField] Vector3 offsetFromCamera = new Vector3(6f, 0f, 0f);

        [Header("Patrouille (nur vertikal)")]
        [SerializeField] float patrolSpeed = 2f;
        [SerializeField] float patrolRangeY = 3f;

        [Header("Schießen")]
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] Transform firePoint;
        [SerializeField] float fireRate = 1.5f;

        [Header("Leben")]
        [SerializeField] int maxHealth = 30;
        int currentHealth;

        float baseY;
        float nextFireTime;
        int direction = 1;

        void Start()
        {
            currentHealth = maxHealth;
            baseY = transform.position.y;
            nextFireTime = Time.time + 1f;
        }

        void Update()
        {
            float targetX = Camera.main.transform.position.x + offsetFromCamera.x;

            baseY += direction * patrolSpeed * Time.deltaTime;
            if (baseY > offsetFromCamera.y + patrolRangeY)
                direction = -1;
            else if (baseY < offsetFromCamera.y - patrolRangeY)
                direction = 1;

            transform.position = new Vector3(targetX, baseY, offsetFromCamera.z);

            if (Time.time >= nextFireTime && bulletPrefab != null)
            {
                nextFireTime = Time.time + fireRate;
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            }
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Debug.Log("Boss besiegt! Sieg!");
                Destroy(gameObject);
            }
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerBullet"))
            {
                TakeDamage(1);
                Destroy(other.gameObject);
            }
        }

        public int GetHealth() => currentHealth;
        public int GetMaxHealth() => maxHealth;
    }
}