using UnityEngine;
using UnityEngine.SceneManagement;

namespace schmup {
    // Steuert das Hauptmenü: Spielstart, Optionen-Panel und Beenden.
    public class MainMenu : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] GameObject optionsPanel;
        [SerializeField] GameObject mainButtonsGroup;

        void Start()
        {
            AudioManager.Instance?.PlayMenuMusic();
        }

        public void PlayGame()
        {
            AudioManager.Instance?.PlayButtonClick();
            ScoreManager.Instance?.ResetScore();
            SceneManager.LoadScene("Level 1");
        }

        public void OpenOptions()
        {
            AudioManager.Instance?.PlayButtonClick();
            if (optionsPanel != null)
                optionsPanel.SetActive(true);
            if (mainButtonsGroup != null)
                mainButtonsGroup.SetActive(false);
        }

        public void CloseOptions()
        {
            AudioManager.Instance?.PlayButtonClick();
            if (optionsPanel != null)
                optionsPanel.SetActive(false);
            if (mainButtonsGroup != null)
                mainButtonsGroup.SetActive(true);
        }

        public void QuitGame()
        {
            AudioManager.Instance?.PlayButtonClick();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
