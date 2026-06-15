using UnityEngine;
namespace schmup{
   
   public class CameraController : MonoBehaviour
   {
       [SerializeField] Transform player; 
       [SerializeField] float speed = 2f;

       void Start(){ 
           transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
       }
       
       void Update() {
           // Bewegung der Kamera entlang des Hintergrundes in der konstanten schnelligkeit
           transform.position += Vector3.right * speed * Time.deltaTime;
           
       }
   }
   
}
