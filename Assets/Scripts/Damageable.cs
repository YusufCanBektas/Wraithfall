using UnityEngine;

namespace schmup
{
    // Basisklasse für alles, was Schaden nehmen und sterben kann (Gegner, Boss).
    // Enemy und BossController erben davon und überschreiben Die() für ihr
    // jeweiliges spezifisches Verhalten (Item-Drop, Level-Ende, etc.).
    public abstract class Damageable : MonoBehaviour
    {
        [Header("Leben")]
        [SerializeField] protected int maxHealth = 3;
        protected int currentHealth;

        protected virtual void Start()
        {
            currentHealth = maxHealth;
        }

        public virtual void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }

        public int GetHealth() => currentHealth;
        public int GetMaxHealth() => maxHealth;
    }
}
