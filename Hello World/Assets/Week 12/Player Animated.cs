using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Week12
{


    public class PlayerAnimated : MonoBehaviour
    {
        [SerializeField]
        public float speed = 5.0f;
        [SerializeField]
        public float currentSpeed;
        [SerializeField]
        float jumpForce = 10f;
        [SerializeField]
        float currentJumpForce;

        public PlayerControls playerControls;

        public bool grounded;
        public bool crouching = false;

        private InputAction move;
        private InputAction look;
        private InputAction jump;
        private InputAction crouch;

        Rigidbody rb;
        Animator animator;

        private void Awake()
        {
            playerControls = new PlayerControls();

            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            move = playerControls.Player.Move;
            jump = playerControls.Player.Jump;
            look = playerControls.Player.Look;
            crouch = playerControls.Player.Crouch;

        }

        private void OnEnable()
        {
            move.Enable();
            look.Enable();

            jump.Enable();
            jump.performed += Jump;

            crouch.Enable();
            crouch.performed += Crouch;

        }

        private void OnDisable()
        {
            move.Disable();
            jump.Disable();
            look.Disable();
            crouch.Disable();
        }



        private void FixedUpdate()
        {
            grounded = IsGrounded();

            HandleMovement();
        }

        void HandleMovement()
        {
            if (crouching) return;
            animator.ResetTrigger("Crouch");

            Vector2 axis = move.ReadValue<Vector2>();

            Vector3 input = (axis.x * transform.right) + (transform.forward * axis.y);

            input *= speed;

            rb.velocity = new Vector3(input.x, rb.velocity.y, input.z);

            if(rb.velocity != Vector3.zero)
            { animator.SetTrigger("Running"); }
            else { animator.ResetTrigger("Running"); }
        }

        bool IsGrounded()
        {
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 3;

            RaycastHit hit;

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up * -1), out hit, 1.1f, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up * -1) * hit.distance, Color.yellow);
                animator.ResetTrigger("Jump");
                return true;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                return false;
            }
        }

        void Jump(InputAction.CallbackContext context)
        {
            if (grounded == false) return;
            crouching = false;
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        void Crouch(InputAction.CallbackContext context)
        {
            if (grounded == false) return;
            animator.SetTrigger("Crouch");
            crouching = true;
        }





    }
}
