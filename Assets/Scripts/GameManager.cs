using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace schmup
{
    // Steuert Sieg/Niederlage: zeigt das passende Panel, verwaltet den
    // Übergang zwischen den beiden Leveln und Retry/Zurück-zum-Menü.
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

            Time.timeScale = 0f;
            ShowFinalScore(gameOverScoreText);

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
        }

        // Wird beim ersten Boss (Level 1) aufgerufen - führt zum Übergang nach Level 2,
        // statt das Spiel direkt zu beenden
        public void OnBossDefeated()
        {
            if (gameEnded) return;
            gameEnded = true;

            if (LevelTransition.Instance != null)
                LevelTransition.Instance.TransitionToScene("Level 2");
            else
                SceneManager.LoadScene("Level 2"); // Fallback ohne Übergangseffekt
        }

        // Wird vom zweiten (finalen) Boss in Level 2 aufgerufen, um das eigentliche Spielende auszulösen
        public void OnFinalBossDefeated()
        {
            if (gameEnded) return;
            gameEnded = true;

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
            ScoreManager.Instance?.ResetScore();
            SceneManager.LoadScene("Level 1");
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f;
            ScoreManager.Instance?.ResetScore();
            SceneManager.LoadScene("MainMenu");
        }

        public bool IsGameEnded() => gameEnded;
    }
}
