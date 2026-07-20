using UnityEngine;

namespace schmup {
    // Lässt die Kamera konstant nach rechts scrollen (klassisches Side-Scroller-Prinzip)
    // und bewegt den zugewiesenen Hintergrund synchron mit.
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float speed = 2f;
        [SerializeField] Transform background;

        bool _isScrolling = true;

        void Update()
        {
            if (!_isScrolling) return;
            transform.position += Vector3.right * (speed * Time.deltaTime);

            // Background mitbewegen, damit er nicht hinter der Kamera zurückbleibt
            if (background != null)
                background.position += Vector3.right * (speed * Time.deltaTime);
        }

        public void StopScrolling() => _isScrolling = false;
        public void StartScrolling() => _isScrolling = true;
    }
}
