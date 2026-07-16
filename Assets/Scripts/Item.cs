using UnityEngine;
using System.Collections;

namespace schmup
{
    public enum ItemType
    {
        Points,
        ExtraLife,
        Shield,
        WeaponUpgrade
    }

    public class Item : MonoBehaviour
    {
        [Header("Typ")]
        [SerializeField] ItemType itemType = ItemType.Points;
        [SerializeField] int pointsValue = 100;

        [Header("Bewegung")]
        [SerializeField] float speed = 1.5f;
        [SerializeField] float lifetime = 15f;

        [Header("Magnet-Effekt")]
        [SerializeField] float magnetRange = 3.5f;
        [SerializeField] float magnetSpeed = 8f;

        [Header("Spawn-Animation")]
        [SerializeField] float popDuration = 0.3f;
        [SerializeField] AnimationCurve popCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        Vector3 targetScale;
        Transform player;

        void Start()
        {
            targetScale = transform.localScale;
            transform.localScale = Vector3.zero;
            StartCoroutine(PopIn());
            Destroy(gameObject, lifetime);

            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }

        IEnumerator PopIn()
        {
            float elapsed = 0f;
            while (elapsed < popDuration)
            {
                elapsed += Time.deltaTime;
                float t = popCurve.Evaluate(Mathf.Clamp01(elapsed / popDuration));
                transform.localScale = targetScale * t;
                yield return null;
            }
            transform.localScale = targetScale;
        }

        void Update()
        {
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.position);
                if (distance <= magnetRange)
                {
                    // Magnet-Effekt: Item wird zum Spieler gezogen
                    transform.position = Vector3.MoveTowards(transform.position, player.position, magnetSpeed * Time.deltaTime);
                    return;
                }
            }

            // Normale Bewegung nach links, wenn kein Magnet-Zug aktiv ist
            transform.position += Vector3.left * (speed * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            switch (itemType)
            {
                case ItemType.Points:
                    ScoreManager.Instance?.AddPoints(pointsValue);
                    break;
                case ItemType.ExtraLife:
                    other.GetComponent<PlayerHealth>()?.AddLife(1);
                    break;
                case ItemType.Shield:
                    other.GetComponent<PlayerHealth>()?.ActivateShield();
                    break;
                case ItemType.WeaponUpgrade:
                    other.GetComponent<PlayerShooter>()?.UpgradeWeapon();
                    break;
            }

            AudioManager.Instance?.PlayItemPickup();
            Destroy(gameObject);
        }
    }
}