using UnityEngine;
using UnityEngine.UI;

namespace schmup
{
    public class PowerUpIndicator : MonoBehaviour
    {
        [Header("Schild-Anzeige")]
        [SerializeField] GameObject shieldIcon;
        [SerializeField] Image shieldCooldownFill;

        [Header("Waffen-Upgrade-Anzeige")]
        [SerializeField] GameObject weaponIcon;
        [SerializeField] Image weaponCooldownFill;

        float shieldTimeRemaining;
        float shieldDuration;
        float weaponTimeRemaining;
        float weaponDuration;

        void Update()
        {
            if (shieldTimeRemaining > 0f)
            {
                shieldTimeRemaining -= Time.deltaTime;
                if (shieldCooldownFill != null)
                    shieldCooldownFill.fillAmount = shieldDuration > 0 ? shieldTimeRemaining / shieldDuration : 0f;

                if (shieldTimeRemaining <= 0f)
                {
                    if (shieldIcon != null) shieldIcon.SetActive(false);
                    if (shieldCooldownFill != null) shieldCooldownFill.gameObject.SetActive(false);
                }
            }

            if (weaponTimeRemaining > 0f)
            {
                weaponTimeRemaining -= Time.deltaTime;
                if (weaponCooldownFill != null)
                    weaponCooldownFill.fillAmount = weaponDuration > 0 ? weaponTimeRemaining / weaponDuration : 0f;

                if (weaponTimeRemaining <= 0f)
                {
                    if (weaponIcon != null) weaponIcon.SetActive(false);
                    if (weaponCooldownFill != null) weaponCooldownFill.gameObject.SetActive(false);
                }
            }
        }

        public void ShowShield(float duration)
        {
            shieldDuration = duration;
            shieldTimeRemaining = duration;
            if (shieldIcon != null) shieldIcon.SetActive(true);
            if (shieldCooldownFill != null) shieldCooldownFill.gameObject.SetActive(true);
        }

        public void ShowWeaponUpgrade(float duration)
        {
            weaponDuration = duration;
            weaponTimeRemaining = duration;
            if (weaponIcon != null) weaponIcon.SetActive(true);
            if (weaponCooldownFill != null) weaponCooldownFill.gameObject.SetActive(true);
        }
    }
}