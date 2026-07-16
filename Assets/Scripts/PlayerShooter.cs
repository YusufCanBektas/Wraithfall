using UnityEngine;

namespace schmup {
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] Transform firePoint;
        [SerializeField] float fireRate = 0.2f;
        [SerializeField] PowerUpIndicator powerUpIndicator;

        [Header("Waffen-Upgrade")]
        [SerializeField] float upgradedFireRate = 0.1f;
        [SerializeField] float upgradeDuration = 8f;
        bool upgraded = false;

        InputReader input;
        float nextFireTime;

        void Start() {
            input = GetComponent<InputReader>();
        }

        void Update() {
            float currentFireRate = upgraded ? upgradedFireRate : fireRate;

            if (input.Shoot && Time.time >= nextFireTime) {
                nextFireTime = Time.time + currentFireRate;
                Vector3 spawnPos = new Vector3(firePoint.position.x, firePoint.position.y, -5f);
                Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                AudioManager.Instance?.PlayShoot();
            }
        }

        public void UpgradeWeapon()
        {
            if (upgraded) return;
            StartCoroutine(UpgradeRoutine());
        }

        System.Collections.IEnumerator UpgradeRoutine()
        {
            upgraded = true;
            Debug.Log("Waffen-Upgrade aktiviert!");
            powerUpIndicator?.ShowWeaponUpgrade(upgradeDuration);
            yield return new WaitForSeconds(upgradeDuration);
            upgraded = false;
            Debug.Log("Waffen-Upgrade abgelaufen.");
        }
    }
}