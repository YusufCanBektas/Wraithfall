using UnityEngine;

namespace schmup {
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] int maxHealth = 3;
        int currentHealth;

        void Start()
        {
            currentHealth = maxHealth;
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Player getroffen von: {other.name} (Tag: {other.tag})");

            // EnemyBullet trifft Spieler
            if (other.CompareTag("EnemyBullet"))
            {
                TakeDamage(1);
                Destroy(other.gameObject);
            }

            // Gegner kollidiert mit Spieler
            if (other.CompareTag("Enemy"))
            {
                TakeDamage(1);
            }
        }

        void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log($"Leben: {currentHealth}");

            if (currentHealth <= 0)
            {
                Debug.Log("Game Over!");
                gameObject.SetActive(false);
            }
        }

        public int GetHealth() => currentHealth;
        public int GetMaxHealth() => maxHealth;
    }
}