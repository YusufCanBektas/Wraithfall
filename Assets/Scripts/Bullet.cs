using UnityEngine;

namespace schmup {
    // Spieler-Projektil. Fliegt nach rechts und fügt jedem getroffenen
    // Damageable-Objekt (Gegner, Boss) Schaden zu.
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float speed = 15f;
        [SerializeField] float lifetime = 3f;

        void Start()
        {
            Destroy(gameObject, lifetime);
        }

        void Update()
        {
            transform.position += Vector3.right * (speed * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            Damageable target = other.GetComponent<Damageable>();
            if (target != null)
            {
                target.TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }
}
