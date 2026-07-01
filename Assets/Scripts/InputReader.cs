using UnityEngine;
using UnityEngine.InputSystem;

namespace schmup {
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour
    {
        PlayerInput playerInput;
        InputAction moveAction;
        InputAction boostAction;
        InputAction shootAction;

        public Vector2 Move => moveAction.ReadValue<Vector2>();
        public bool Boost => boostAction.IsPressed();
        public bool Shoot => shootAction.IsPressed();

        void Start() {
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
            boostAction = playerInput.actions["Boost"];
            shootAction = playerInput.actions["shoot"];
        }
    }
}