using UnityEngine;
namespace schmup{
    public class ParallaxController : MonoBehaviour
    {
        [SerializeField]  Transform[] backgrounds; // Array von dem Hintergrund Ebenen 
        [SerializeField]  float smoothing = 10f; // Wie geschmeidig das Parallax effect ist 
        [SerializeField]  float multiplier = 15f; // Wie viel das Parallax effect levels per Ebene ist

         Transform cam; // Referenz von der Main Camera
         Vector3 previousCamPos; // Kamera Position vorherige frame 

         void Awake() => cam = Camera.main.transform;

         void Start() => previousCamPos = cam.position;

         void Update() {
             // Wiederholung der Hintergrund ebenen
             for (var i = 0; i < backgrounds.Length; i++)
             {
                 var parallax = (previousCamPos.y - cam.position.y) * (i * multiplier);
                 var targetY = backgrounds[i].position.y + parallax;

                 var targetPosition =
                     new Vector3(backgrounds[i].position.x, targetY, backgrounds[i].position.z);
                 backgrounds[i].position =
                     Vector3.Lerp(backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);
             }

             previousCamPos = cam.position;
         }
    }
}
