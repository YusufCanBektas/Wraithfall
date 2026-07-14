using UnityEngine;
using UnityEngine.SceneManagement;

namespace schmup
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("UI Panels")]
        [SerializeField] GameObject gameOverPanel;
        [SerializeField] GameObject winPanel;

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

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
        }

        public void OnBossDefeated()
        {
            if (gameEnded) return;
            gameEnded = true;

            Debug.Log("Sieg!");
            Time.timeScale = 0f;

            if (winPanel != null)
                winPanel.SetActive(true);
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
