using UnityEngine;
using UnityEngine.InputSystem;

namespace schmup
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour
    {
        PlayerInput playerInput;
        InputAction moveAction;
        InputAction boostAction;

        public Vector2 Move => moveAction.ReadValue<Vector2>();
        public bool Boost => boostAction.IsPressed();

        void Start() {
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
            boostAction = playerInput.actions["Boost"];
        }
    }
}