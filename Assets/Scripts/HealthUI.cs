using UnityEngine;
using UnityEngine.UI;

namespace schmup
{
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
