using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace schmup
{
    public class LevelTransition : MonoBehaviour
    {
        public static LevelTransition Instance { get; private set; }

        [Header("Übergangs-Bild")]
        [SerializeField] CanvasGroup fadeCanvasGroup; // schwarzes Vollbild-Panel mit CanvasGroup
        [SerializeField] float fadeOutDuration = 1.5f;
        [SerializeField] float holdDuration = 1f;
        [SerializeField] float fadeInDuration = 1f;

        [Header("Kamera-Zoom-Effekt")]
        [SerializeField] Camera mainCamera;
        [SerializeField] float zoomFieldOfView = 15f; // niedrigerer Wert = reinzoomen
        [SerializeField] float zoomDuration = 1.5f;

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

        public void TransitionToScene(string sceneName)
        {
            StartCoroutine(TransitionRoutine(sceneName));
        }

        IEnumerator TransitionRoutine(string sceneName)
        {
            float originalFOV = mainCamera != null ? mainCamera.fieldOfView : 60f;

            // Reinzoomen (Gefühl von "ins All fliegen")
            if (mainCamera != null)
            {
                float elapsed = 0f;
                while (elapsed < zoomDuration)
                {
                    elapsed += Time.deltaTime;
                    mainCamera.fieldOfView = Mathf.Lerp(originalFOV, zoomFieldOfView, elapsed / zoomDuration);
                    yield return null;
                }
            }

            // Ausblenden (schwarz)
            if (fadeCanvasGroup != null)
            {
                fadeCanvasGroup.gameObject.SetActive(true);
                float elapsed = 0f;
                while (elapsed < fadeOutDuration)
                {
                    elapsed += Time.deltaTime;
                    fadeCanvasGroup.alpha = Mathf.Clamp01(elapsed / fadeOutDuration);
                    yield return null;
                }
                fadeCanvasGroup.alpha = 1f;
            }

            yield return new WaitForSeconds(holdDuration);

            // Neue Szene laden
            SceneManager.LoadScene(sceneName);
            yield return null; // einen Frame warten, bis die neue Szene aktiv ist

            // Kamera in der neuen Szene wieder finden und FOV zurücksetzen
            Camera newCamera = Camera.main;
            if (newCamera != null)
                newCamera.fieldOfView = originalFOV;

            // Wieder einblenden
            if (fadeCanvasGroup != null)
            {
                float elapsed = 0f;
                while (elapsed < fadeInDuration)
                {
                    elapsed += Time.deltaTime;
                    fadeCanvasGroup.alpha = 1f - Mathf.Clamp01(elapsed / fadeInDuration);
                    yield return null;
                }
                fadeCanvasGroup.alpha = 0f;
                fadeCanvasGroup.gameObject.SetActive(false);
            }
        }
    }
}