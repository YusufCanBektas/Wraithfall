using UnityEngine;

namespace schmup {
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] Transform firePoint;
        [SerializeField] float fireRate = 0.2f;

        InputReader input;
        float nextFireTime;

        void Start() {
            input = GetComponent<InputReader>();
        }

        void Update() {
            if (input.Shoot && Time.time >= nextFireTime) {
                nextFireTime = Time.time + fireRate;
                Vector3 spawnPos = new Vector3(firePoint.position.x, firePoint.position.y, -5f);
                Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                AudioManager.Instance?.PlayShoot();
            }
        }
    }
}