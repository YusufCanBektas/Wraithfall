using UnityEngine;

namespace schmup {
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
            Debug.Log($"Bullet trifft: {other.name}");

            Damageable target = other.GetComponent<Damageable>();
            if (target != null)
            {
                target.TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }
}