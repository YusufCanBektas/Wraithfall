using UnityEngine;

namespace schmup {
    // Verwaltet die Leben des Spielers, reagiert auf Treffer durch Gegner-Projektile
    // oder Kollision mit Gegnern, und steuert das temporäre Schild-Power-Up.
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
            if (other.CompareTag("EnemyBullet"))
            {
                TakeDamage(1);
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Enemy"))
            {
                TakeDamage(1);
            }
        }

        void TakeDamage(int damage)
        {
            // Solange das Schild aktiv ist, wird jeder Schaden ignoriert
            if (shieldActive) return;

            currentHealth -= damage;
            AudioManager.Instance?.PlayHit();
            healthUI?.UpdateDisplay(currentHealth);

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                GameManager.Instance?.OnPlayerDied();
            }
        }

        public void AddLife(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
            healthUI?.UpdateDisplay(currentHealth);
        }

        public void ActivateShield()
        {
            if (shieldActive) return;
            StartCoroutine(ShieldRoutine());
        }

        System.Collections.IEnumerator ShieldRoutine()
        {
            shieldActive = true;
            powerUpIndicator?.ShowShield(shieldDuration);
            yield return new WaitForSeconds(shieldDuration);
            shieldActive = false;
        }

        public int GetHealth() => currentHealth;
        public int GetMaxHealth() => maxHealth;
    }
}
