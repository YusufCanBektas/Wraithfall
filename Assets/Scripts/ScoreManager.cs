using UnityEngine;

namespace schmup
{
    // Verwaltet die aktuelle Punktzahl und den dauerhaft gespeicherten Highscore.
    // Bleibt als Singleton über Szenenwechsel (Level 1 -> Level 2) hinweg bestehen,
    // damit der Punktestand über beide Level hinweg erhalten bleibt.
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        const string HighscoreKey = "Highscore";

        int currentScore = 0;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void AddPoints(int amount)
        {
            currentScore += amount;
        }

        public void ResetScore()
        {
            currentScore = 0;
        }

        public int GetScore() => currentScore;

        public int GetHighscore() => PlayerPrefs.GetInt(HighscoreKey, 0);

        // Prüft, ob der aktuelle Score ein neuer Highscore ist, und speichert ihn ggf.
        // Gibt true zurück, wenn ein neuer Highscore erreicht wurde.
        public bool CheckAndSaveHighscore()
        {
            int savedHighscore = GetHighscore();
            if (currentScore > savedHighscore)
            {
                PlayerPrefs.SetInt(HighscoreKey, currentScore);
                PlayerPrefs.Save();
                return true;
            }
            return false;
        }
    }
}
