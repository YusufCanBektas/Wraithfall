using UnityEngine;

namespace schmup
{
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
        }

        void Start()
        {
            currentScore = 0;
        }

        public void AddPoints(int amount)
        {
            currentScore += amount;
            Debug.Log($"Punkte: {currentScore}");
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
                Debug.Log($"Neuer Highscore: {currentScore}!");
                return true;
            }
            return false;
        }
    }
}