using UnityEngine;

namespace schmup {
    public class BackgroundLooper : MonoBehaviour
    {
        [SerializeField] Transform cam;
        [SerializeField] float backgroundWidth = 28f;

        GameObject copyB;
        bool initialized = false;

        void Start()
        {
            // Nur einmal eine Kopie erstellen
            copyB = Instantiate(gameObject, transform.parent);
            copyB.GetComponent<BackgroundLooper>().enabled = false; // Script auf Kopie deaktivieren!
            copyB.transform.position = new Vector3(transform.position.x + backgroundWidth, transform.position.y, transform.position.z);
            initialized = true;
        }

        void Update()
        {
            if (!initialized) return;

            // Früher repositionieren: sobald der Hintergrund halb durch ist
            if (transform.position.x < cam.position.x - backgroundWidth * 0.5f)
            {
                transform.position = new Vector3(copyB.transform.position.x + backgroundWidth, transform.position.y, transform.position.z);
            }

            if (copyB.transform.position.x < cam.position.x - backgroundWidth * 0.5f)
            {
                copyB.transform.position = new Vector3(transform.position.x + backgroundWidth, copyB.transform.position.y, copyB.transform.position.z);
            }
        }
    }
}