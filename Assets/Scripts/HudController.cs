using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace schmup
{
    // Zentrale Steuerung für die Spiel-UI während eines Levels:
    // Punkteanzeige, Wellenzähler und die Boss-Healthbar.
    public class HUDController : MonoBehaviour
    {
        [Header("Score")]
        [SerializeField] TextMeshProUGUI scoreText;

        [Header("Welle")]
        [SerializeField] TextMeshProUGUI waveText;
        [SerializeField] int totalWaves = 5;

        [Header("Boss-Healthbar")]
        [SerializeField] GameObject bossHealthBarContainer;
        [SerializeField] Slider bossHealthSlider;
        [SerializeField] TextMeshProUGUI bossNameText;

        WaveSpawner waveSpawner;

        void Start()
        {
            waveSpawner = FindFirstObjectByType<WaveSpawner>();
            if (bossHealthBarContainer != null)
                bossHealthBarContainer.SetActive(false);
        }

        void Update()
        {
            UpdateScore();
            UpdateWave();
        }

        void UpdateScore()
        {
            if (scoreText == null || ScoreManager.Instance == null) return;
            scoreText.text = $"Punkte: {ScoreManager.Instance.GetScore()}";
        }

        void UpdateWave()
        {
            if (waveText == null || waveSpawner == null) return;
            int current = Mathf.Min(waveSpawner.GetCurrentWaveNumber(), totalWaves);
            waveText.text = $"Welle {current}/{totalWaves}";
        }

        // Wird vom BossController aufgerufen, sobald der Boss spawnt
        public void ShowBossHealthBar(BossController boss)
        {
            if (bossHealthBarContainer != null)
                bossHealthBarContainer.SetActive(true);

            if (bossNameText != null)
                bossNameText.text = "BOSS";

            UpdateBossHealth(boss);
        }

        public void UpdateBossHealth(BossController boss)
        {
            if (bossHealthSlider == null || boss == null) return;
            bossHealthSlider.maxValue = boss.GetMaxHealth();
            bossHealthSlider.value = boss.GetHealth();
        }

        public void HideBossHealthBar()
        {
            if (bossHealthBarContainer != null)
                bossHealthBarContainer.SetActive(false);
        }
    }
}
