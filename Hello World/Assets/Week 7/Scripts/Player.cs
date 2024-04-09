using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TMPro;
using UnityEngine.SceneManagement;

namespace Week7
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        float speed = 5.0f;
        [SerializeField]
        float currentSpeed;
        [SerializeField]
        float rotation = 5.0f;
        [SerializeField]
        float currentRotation;
        [SerializeField]
        float jumpForce = 10f;
        [SerializeField]
        float currentJumpForce;

        [SerializeField]
        GameObject bulletPrefab;
        [SerializeField]
        Transform gunTransform;

        [SerializeField]
        int keys = 0;
        [SerializeField]
        int coins = 0;
        [SerializeField]
        int health = 100;
        [SerializeField]
        int healingPickupStrength = 5;
        [SerializeField]
        int maxHealth = 100;

        [SerializeField]
        TextMeshProUGUI keyText;
        [SerializeField]
        TextMeshProUGUI coinText;
        [SerializeField]
        TextMeshProUGUI healthText;
        [SerializeField]
        GameObject RestartButton;

        public PlayerControls playerControls;

        private float mouseDeltaX = 0f;
        private float mouseDeltaY = 0f;
        private float cameraRotX = 0f;
        private int rotDir = 0;
        private bool grounded;

        private InputAction move;
        private InputAction look;
        private InputAction jump;
        private InputAction fire;

        Rigidbody rb;

        private void Awake()
        {
            playerControls = new PlayerControls();

            rb = GetComponent<Rigidbody>();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            move = playerControls.Player.Move;
            jump = playerControls.Player.Jump;
            look = playerControls.Player.Look;
            fire = playerControls.Player.Fire;

            health = maxHealth;
            currentSpeed = speed;
            currentRotation = rotation;
            currentJumpForce = jumpForce;
        }

        private void OnEnable()
        {
            move.Enable();
            look.Enable();

            jump.Enable();
            jump.performed += Jump;

            fire.Enable();
            fire.performed += Fire;
        }

        private void OnDisable()
        {
            move.Disable();
            jump.Disable();
            look.Disable();
            fire.Disable();
        }


        private void Update()
        {
            HandleHorizontalRotation();
            HandleVerticalRotation();

            healthText.text = $"Health: {health}";
            if (health <= 0) 
            { 
                RestartButton.SetActive(true);
                speed = 0f;
                rotation = 0f;
                jumpForce = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                rb.constraints = RigidbodyConstraints.FreezePositionY;
            }
            if (transform.position.y < -400) { health = 0; }
        }

        private void FixedUpdate()
        {
            grounded = IsGrounded();

            HandleMovement();
        }

        void HandleMovement()
        {
            if (grounded == false) return;

            Vector2 axis = move.ReadValue<Vector2>();

            Vector3 input = (axis.x * transform.right) + (transform.forward * axis.y);

            input *= speed;

            rb.velocity = new Vector3(input.x, rb.velocity.y, input.z);
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
                return true;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                return false;
            }
        }

        void HandleHorizontalRotation()
        {

            mouseDeltaX = look.ReadValue<Vector2>().x;

            if (mouseDeltaX != 0)
            {
                rotDir = mouseDeltaX > 0 ? 1 : -1;

                transform.eulerAngles += new Vector3(0, rotation * Time.deltaTime * rotDir, 0);
            }
        }

        void HandleVerticalRotation()
        {
            mouseDeltaY = look.ReadValue<Vector2>().y;

            if (mouseDeltaY != 0)
            {
                rotDir = mouseDeltaY > 0 ? -1 : 1;

                cameraRotX += rotation * Time.deltaTime * rotDir;
                cameraRotX = Mathf.Clamp(cameraRotX, -45f, 90f);

                var targetRotation = Quaternion.Euler(Vector3.right * cameraRotX);


                //Vector3 angle = new Vector3(rotation * Time.deltaTime * rotDir, 0, 0);

                //Debug.Log(Camera.main.transform.localRotation.x);

                Camera.main.transform.localRotation = targetRotation;
                //Camera.main.transform.Rotate(angle, Space.Self);

            }
        }

        void Jump(InputAction.CallbackContext context)
        {
            if (grounded == false) return;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        void Fire(InputAction.CallbackContext context)
        {
            if (health > 0)
            {
                Instantiate(bulletPrefab, gunTransform.position, Camera.main.transform.rotation);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Key"))
            {
                Debug.Log("Collided with Key");
                keys++;
                collider.gameObject.SetActive(false);
                keyText.text = $"Keys: {keys.ToString()}";
            }
            if (collider.CompareTag("Coin"))
            {
                Debug.Log("Collided with Coin");
                coins++;
                collider.gameObject.SetActive(false);
                coinText.text = $"Coins: {coins.ToString()}";
                health += healingPickupStrength;

                if (health > maxHealth) { health = maxHealth; }
            }
            if (collider.CompareTag("LockedDoorTrigger"))
            {
                Debug.Log("Collided with Door");
                if(keys > 0)
                {
                    collider.GetComponent<LockedDoorTrigger>().UnlockDoor();
                    keys--;
                    keyText.text = $"Keys: {keys.ToString()}";
                }
            }
        }
        private void OnTriggerStay(Collider collider)
        {
            if (collider.CompareTag("Trap"))
            {
                Debug.Log("Collided with Trap");
                if (health > 0)
                {
                    health--;
                }
            }
        }

        public void RestartGame()
        {
            speed = currentSpeed;
            rotation = currentRotation;
            jumpForce = currentJumpForce;
            keys = 0;
            coins = 0;
            health = maxHealth;

            transform.position = new Vector3(0, 3, 0);
            rb.constraints = ~RigidbodyConstraints.FreezePositionY;
            

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            RestartButton.SetActive(false);
            keyText.text = $"Keys: {keys.ToString()}";
            coinText.text = $"Coins: {coins.ToString()}";
        }
    }
}
