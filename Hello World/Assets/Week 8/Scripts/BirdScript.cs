using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Week8
{
    public class BirdScript : MonoBehaviour
    {
        [SerializeField] Rigidbody2D myRigidBody;
        [SerializeField] LogicScript logic;
        [SerializeField] float flapStrength;

        private bool isGrounded = false;
        public bool birdIsAlive = true;

        public PlayerControls playerControls;
        private InputAction flap; 

        [SerializeField] Animator animator;

        [SerializeField] AudioSource flapSFX;
        [SerializeField] AudioSource crashSFX;
        
        private void Awake()
        {
            playerControls = new PlayerControls();
            myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;

            flap = playerControls.Bird.Flap;
        }

        private void OnEnable()
        {
            flap.Enable();
            flap.performed += Flap;
        }
        private void OnDisable()
        {
            flap.Disable();
        }

        void Flap(InputAction.CallbackContext context)
        {
            Debug.Log("Flap!");
            // Make bird flap and if game isn't started start the game
            if (birdIsAlive == true)
            {
                flapSFX.Play();
                myRigidBody.velocity = Vector2.up * flapStrength;
                animator.SetTrigger("Flap");
                if (logic.startGameCheck == false)
                {
                    myRigidBody.constraints = RigidbodyConstraints2D.None;
                    logic.startGame();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Kill bird if it moves too high or too low on screen and then freeze it's Y position on the bottom so it doesn't fall forever
            if (transform.position.y > 17 || transform.position.y < -17)
            {
                if (birdIsAlive) { crashSFX.Play(); }
                animator.SetTrigger("Crash");
                birdIsAlive = false;
                logic.gameOver();
            }
            if (transform.position.y < -17)
            {
                animator.SetTrigger("Crash");
                myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
            //Move bird off screen and then stay there once dead
            if (!birdIsAlive)
            {
                if (transform.position.x > -30)
                {
                    transform.position = transform.position + (Vector3.left * logic.moveSpeed) * Time.deltaTime;
                }
                else
                {
                    myRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
                }
            }
            if (isGrounded)
            {
                myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
            }
        }
        //Kill bird if it crashes into something
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (birdIsAlive) { crashSFX.Play(); }
            animator.SetTrigger("Crash");
            myRigidBody.velocity = new Vector2(-5, 0);
            birdIsAlive = false;
            logic.gameOver();

            if (collision.gameObject.tag == ("Ground"))
            {
                isGrounded = true;
                animator.speed = 0;
            }
        }
    }
}
