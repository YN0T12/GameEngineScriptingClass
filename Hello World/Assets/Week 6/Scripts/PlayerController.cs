using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Week6
{
    public class PlayerController : MonoBehaviour
    {
        InputAction moveAction;
        InputAction jumpAction;
        InputAction fireAction;
        PlayerControllerMappings mappings;

        Rigidbody rb;

        [SerializeField] float speedStrength = 5f;
        [SerializeField] float jumpStrength = 5f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            mappings = new PlayerControllerMappings();

            moveAction = mappings.Player.Move;
            jumpAction = mappings.Player.Jump;
            fireAction = mappings.Player.Fire;

            jumpAction.performed += Jump;
        }

        private void OnEnable()
        {
            moveAction.Enable();
            jumpAction.Enable();
            fireAction.Enable();
        }
        private void OnDisable()
        {
            moveAction.Disable();
            jumpAction.Disable();
            fireAction.Disable();
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            input *= speedStrength;

            rb.velocity = new Vector3(input.x, rb.velocity.y, input.y);
            //transform.rotation *= Quaternion.Euler(0, transform.rotation.y + input.x, 0);
        }

        void Jump(InputAction.CallbackContext context)
        {
            Debug.Log("jump wow");
            if (!IsGrounded()) return;
            rb.AddForce(Vector3.up * jumpStrength);
        }

        bool IsGrounded()
        {
            int layerMask = 1 << 3;

            RaycastHit hit;

            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up * -1), out hit, 1.1f, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up * -1) * hit.distance, Color.yellow);
                return true;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up * -1) * hit.distance, Color.white);
                return false;
            }
            return false;
        }
    }
}

