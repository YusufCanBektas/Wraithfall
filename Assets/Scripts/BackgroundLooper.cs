using UnityEngine;

namespace schmup {
    public class BackgroundLooper : MonoBehaviour
    {
        [SerializeField] Transform cam;
        [SerializeField] float backgroundWidth = 43f;

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

            // Wenn Original links aus dem Bild
            if (transform.position.x < cam.position.x - backgroundWidth)
            {
                transform.position = new Vector3(copyB.transform.position.x + backgroundWidth, transform.position.y, transform.position.z);
            }

            // Wenn Kopie links aus dem Bild
            if (copyB.transform.position.x < cam.position.x - backgroundWidth)
            {
                copyB.transform.position = new Vector3(transform.position.x + backgroundWidth, copyB.transform.position.y, copyB.transform.position.z);
            }
        }
    }
}