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
        [SerializeField] float normalFieldOfView = 60f; // Standard-FOV, auf den nach dem Übergang zurückgesetzt wird
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
            // Reinzoomen (Gefühl von "ins All fliegen")
            if (mainCamera != null)
            {
                float startFOV = mainCamera.fieldOfView;
                float elapsed = 0f;
                while (elapsed < zoomDuration)
                {
                    elapsed += Time.deltaTime;
                    mainCamera.fieldOfView = Mathf.Lerp(startFOV, zoomFieldOfView, elapsed / zoomDuration);
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

            // Neue Szene laden und WIRKLICH warten, bis sie fertig geladen ist
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName);
            while (!loadOp.isDone)
            {
                yield return null;
            }
            yield return null; // einen zusätzlichen Frame, damit Awake/Start der neuen Szene durchgelaufen sind

            // Kamera in der neuen Szene wieder finden und FOV zurücksetzen
            Camera newCamera = Camera.main;
            if (newCamera != null)
                newCamera.fieldOfView = normalFieldOfView;
            else
                Debug.LogWarning("LevelTransition: Keine Main Camera in der neuen Szene gefunden, FOV konnte nicht zurückgesetzt werden.");

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