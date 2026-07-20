using UnityEngine;

namespace schmup {
    // Steuert die Spieler-Bewegung innerhalb der Kamera-Grenzen sowie den
    // Lean-Effekt (Schräglage des Schiffs je nach Bewegungsrichtung).
    public class PlayerController : MonoBehaviour {

        [SerializeField] float speed = 5f;
        [SerializeField] float boostMultiplier = 2f;
        [SerializeField] float smoothness = 0.1f;
        [SerializeField] float leanAngle = 15f;
        [SerializeField] float leanSpeed = 5f;

        [SerializeField] GameObject model;

        [Header("Camera Bounds")]
        [SerializeField] Transform cameraTransform;
        [SerializeField] float minX = -8f;
        [SerializeField] float maxX = 8f;
        [SerializeField] float minY = -4f;
        [SerializeField] float maxY = 4f;

        InputReader input;

        Vector3 _currentVelocity;
        Vector3 _targetPosition;

        void Start() {
            input = GetComponent<InputReader>();
            _targetPosition = transform.position;
        }

        void Update() {
            // Boost mit Shift
            bool isBoosting = input.Boost;
            float currentSpeed = speed * (isBoosting ? boostMultiplier : 1f);

            // Bewegung: hoch/runter (Y) + boost vor/zurück (X)
            Vector3 moveInput = new Vector3(input.Move.x, input.Move.y, 0f);
            _targetPosition += moveInput * (currentSpeed * Time.deltaTime);

            // Bewegung wird auf den Bereich um die Kamera herum begrenzt,
            // damit der Spieler nicht aus dem sichtbaren Bild fliegen kann
            var minPlayerX = cameraTransform.position.x + minX;
            var maxPlayerX = cameraTransform.position.x + maxX;
            var minPlayerY = cameraTransform.position.y + minY;
            var maxPlayerY = cameraTransform.position.y + maxY;

            _targetPosition.x = Mathf.Clamp(_targetPosition.x, minPlayerX, maxPlayerX);
            _targetPosition.y = Mathf.Clamp(_targetPosition.y, minPlayerY, maxPlayerY);

            transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _currentVelocity, smoothness, Mathf.Infinity);

            // Lean auf Z-Achse (für Side-Scroller korrekt)
            float targetLean = -input.Move.y * leanAngle;
            float currentLean = transform.localEulerAngles.z;
            // Winkel normalisieren (Unity gibt 0-360 zurück)
            if (currentLean > 180f) currentLean -= 360f;
            float newLean = Mathf.Lerp(currentLean, targetLean, leanSpeed * Time.deltaTime);

            transform.localEulerAngles = new Vector3(0f, 0f, newLean);
        }
    }
}
