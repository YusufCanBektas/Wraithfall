using UnityEngine;

namespace schmup {
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] int maxHealth = 5;
        [SerializeField] HealthUI healthUI;
        int currentHealth;

        void Start()
        {
            currentHealth = maxHealth;
            healthUI?.UpdateDisplay(currentHealth);
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

            AudioManager.Instance?.PlayHit();
            healthUI?.UpdateDisplay(currentHealth);

            if (currentHealth <= 0)
            {
                Debug.Log("Game Over!");
                gameObject.SetActive(false);
                GameManager.Instance?.OnPlayerDied();
            }
        }

        public int GetHealth() => currentHealth;
        public int GetMaxHealth() => maxHealth;
    }
}