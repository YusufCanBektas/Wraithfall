using UnityEngine;

namespace schmup {
    public class ParallaxController : MonoBehaviour
    {
        [SerializeField] Transform[] backgrounds;
        [SerializeField] float[] scrollSpeeds;

        bool isScrolling = true;
        float[] startPositionsX;
        float[] spriteWidths;

        void Start()
        {
            startPositionsX = new float[backgrounds.Length];
            spriteWidths = new float[backgrounds.Length];

            for (var i = 0; i < backgrounds.Length; i++)
            {
                startPositionsX[i] = backgrounds[i].position.x;

                // Breite automatisch pro Layer auslesen
                var sr = backgrounds[i].GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    spriteWidths[i] = sr.bounds.size.x;
                }
                else
                {
                    // Fallback falls kein SpriteRenderer
                    var renderer = backgrounds[i].GetComponent<Renderer>();
                    spriteWidths[i] = renderer != null ? renderer.bounds.size.x : 10f;
                }

                Debug.Log($"Layer {i} ({backgrounds[i].name}): Breite = {spriteWidths[i]}");
            }
        }

        void Update()
        {
            if (!isScrolling) return;

            for (var i = 0; i < backgrounds.Length; i++)
            {
                float speed = (scrollSpeeds != null && i < scrollSpeeds.Length)
                    ? scrollSpeeds[i]
                    : (i + 1) * 0.5f;

                backgrounds[i].position += Vector3.left * (speed * Time.deltaTime);

                if (backgrounds[i].position.x < startPositionsX[i] - spriteWidths[i])
                {
                    backgrounds[i].position = new Vector3(
                        startPositionsX[i],
                        backgrounds[i].position.y,
                        backgrounds[i].position.z
                    );
                }
            }
        }

        public void StopScrolling() => isScrolling = false;
        public void StartScrolling() => isScrolling = true;
    }
}