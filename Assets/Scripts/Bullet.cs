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

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
                Destroy(gameObject);
                return;
            }

            BossController boss = other.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }
}