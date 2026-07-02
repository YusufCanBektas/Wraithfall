using UnityEngine;

namespace schmup {
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        [SerializeField] float lifetime = 4f;

        void Start()
        {
            Destroy(gameObject, lifetime);
        }

        void Update()
        {
            transform.position += Vector3.left * (speed * Time.deltaTime);
        }
    }
}