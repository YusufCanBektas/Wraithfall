using UnityEngine;
using UnityEngine.SceneManagement;

namespace schmup {
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}