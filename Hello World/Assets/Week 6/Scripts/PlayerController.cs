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

        [SerializeField] float speedStrength = 8;
        [SerializeField] float tiredSpeedStrength = 3;
        [SerializeField] float jumpStrength = 3;

        [SerializeField] float stamina = 100f;
        [SerializeField] float maxStamina = 100f;

        [SerializeField] float runCost = 1f;

        [SerializeField] float sensX = 200f;
        [SerializeField] float sensY = 200f;

        [SerializeField] Transform orientation;

        float xRot;
        float yRot;

        float timer = 0.0f;
        float interlopeTime = 0.1f;
        float regainingStaminaInterlopeTime = 0.2f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;

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
            
            //refilling stamina 
            if ((input.x == 0 || input.y == 0) && stamina < maxStamina)
            {
                timer += Time.deltaTime;

                if (timer >= regainingStaminaInterlopeTime)
                {
                    timer = 0.0f;

                    stamina++;
                }
                
            }
            //using stamina
            if (input.x != 0 || input.y != 0)
            {
                timer += Time.deltaTime;

                if (timer >= interlopeTime)
                {
                    timer = 0.0f;

                    stamina -= runCost;
                }

                if (stamina < 0) stamina = 0;
            }
            float speed = speedStrength;
            //Setting tired speed if stamina is 0
            if (stamina == 0)
            {
                speed = tiredSpeedStrength;
            }
            else speed = speedStrength;

            input *= speed;
            rb.velocity = new Vector3(input.x, rb.velocity.y, input.y);
            //rb.rotation = Quaternion.AngleAxis(rb.rotation, rb.velocity);

            //Camera controls
            /*float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRot += mouseX;
            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRot, yRot, 0);
            orientation.rotation = Quaternion.Euler(0, yRot, 0);*/
        }

        void Jump(InputAction.CallbackContext context)
        {
            //Debug.Log("jumped");
            //if (!IsGrounded()) return;
            rb.AddForce(Vector3.up * jumpStrength * 100);
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

