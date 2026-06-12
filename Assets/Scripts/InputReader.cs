using UnityEngine;
using UnityEngine.InputSystem;

namespace schmup
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour
    {
        // Sei dir sicher, dass du die Player input komponente bei c# events einsetzt
         PlayerInput playerInput;
         InputAction moveAction;

         public Vector2 Move => moveAction.ReadValue<Vector2>();

         void Start() {
             playerInput = GetComponent<PlayerInput>();
             moveAction = playerInput.actions["Move"];
         }
    }
}

