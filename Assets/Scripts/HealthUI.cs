using UnityEngine;

namespace schmup
{
    // Steuert die Anzeige der Spieler-Leben als Reihe von Icons.
    // Icon i ist sichtbar, solange currentHealth größer als sein Index ist.
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] GameObject[] healthIcons; // in der Reihenfolge im Inspector zuweisen

        public void UpdateDisplay(int currentHealth)
        {
            for (int i = 0; i < healthIcons.Length; i++)
            {
                if (healthIcons[i] != null)
                    healthIcons[i].SetActive(i < currentHealth);
            }
        }
    }
}
