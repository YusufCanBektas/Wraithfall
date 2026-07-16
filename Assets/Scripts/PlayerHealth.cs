using UnityEngine;

namespace schmup {
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] int maxHealth = 5;
        [SerializeField] HealthUI healthUI;
        [SerializeField] PowerUpIndicator powerUpIndicator;
        int currentHealth;

        [Header("Schild")]
        [SerializeField] float shieldDuration = 5f;
        bool shieldActive = false;

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
            if (shieldActive)
            {
                Debug.Log("Schild blockiert Schaden!");
                return;
            }

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

        public void AddLife(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
            healthUI?.UpdateDisplay(currentHealth);
            Debug.Log($"Extra-Leben! Leben: {currentHealth}");
        }

        public void ActivateShield()
        {
            if (shieldActive) return;
            StartCoroutine(ShieldRoutine());
        }

        System.Collections.IEnumerator ShieldRoutine()
        {
            shieldActive = true;
            Debug.Log("Schild aktiviert!");
            powerUpIndicator?.ShowShield(shieldDuration);
            yield return new WaitForSeconds(shieldDuration);
            shieldActive = false;
            Debug.Log("Schild abgelaufen.");
        }

        public int GetHealth() => currentHealth;
        public int GetMaxHealth() => maxHealth;
    }
}