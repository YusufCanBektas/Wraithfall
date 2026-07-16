using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace schmup
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("UI Panels")]
        [SerializeField] GameObject gameOverPanel;
        [SerializeField] GameObject winPanel;

        [Header("Score-Anzeige auf den Panels")]
        [SerializeField] TextMeshProUGUI gameOverScoreText;
        [SerializeField] TextMeshProUGUI winScoreText;

        bool gameEnded = false;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void OnPlayerDied()
        {
            if (gameEnded) return;
            gameEnded = true;

            Debug.Log("Game Over!");
            Time.timeScale = 0f;

            ShowFinalScore(gameOverScoreText);

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
        }

        public void OnBossDefeated()
        {
            if (gameEnded) return;
            gameEnded = true;

            Debug.Log("Sieg!");
            Time.timeScale = 0f;

            ShowFinalScore(winScoreText);

            if (winPanel != null)
                winPanel.SetActive(true);
        }

        void ShowFinalScore(TextMeshProUGUI scoreText)
        {
            if (ScoreManager.Instance == null) return;

            int finalScore = ScoreManager.Instance.GetScore();
            bool isNewHighscore = ScoreManager.Instance.CheckAndSaveHighscore();
            int highscore = ScoreManager.Instance.GetHighscore();

            if (scoreText != null)
            {
                scoreText.text = isNewHighscore
                    ? $"Punkte: {finalScore}\nNeuer Highscore!"
                    : $"Punkte: {finalScore}\nHighscore: {highscore}";
            }
        }

        public void RetryLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}