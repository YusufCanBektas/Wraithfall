using UnityEngine;
using UnityEngine.InputSystem;

namespace schmup {
    // Liest die Eingaben aus dem Unity Input System aus und stellt sie
    // als einfache Properties für PlayerController und PlayerShooter bereit.
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
