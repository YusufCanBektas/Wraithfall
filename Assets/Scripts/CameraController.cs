using UnityEngine;

namespace schmup {
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float speed = 2f;
        [SerializeField] Transform background;

        bool _isScrolling = true;

        void Update()
        {
            if (!_isScrolling) return;
            transform.position += Vector3.right * (speed * Time.deltaTime);
            
            // Background mitbewegen
            if (background != null)
                background.position += Vector3.right * (speed * Time.deltaTime);
        }

        public void StopScrolling() => _isScrolling = false;
        public void StartScrolling() => _isScrolling = true;
    }
}