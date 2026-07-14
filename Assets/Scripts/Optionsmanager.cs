using UnityEngine;
using UnityEngine.UI;

namespace schmup {
    public class OptionsManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] Slider volumeSlider;

        void Start()
        {
            float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
            if (volumeSlider != null)
            {
                volumeSlider.value = savedVolume;
                volumeSlider.onValueChanged.AddListener(SetVolume);
            }
        }

        public void SetVolume(float value)
        {
            AudioManager.Instance?.SetVolume(value);
        }
    }
}