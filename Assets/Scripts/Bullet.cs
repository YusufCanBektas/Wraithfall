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
    }
}