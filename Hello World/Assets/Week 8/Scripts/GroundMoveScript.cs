using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week8
{
    public class GroundMoveScript : MonoBehaviour
    {
        float deadZone = -30;
        [SerializeField] LogicScript logic;

        // Start is called before the first frame update
        void Start()
        {
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        }

        // Update is called once per frame
        void Update()
        {

            // This will move the Ground's transform position to the left at "moveSpeed" at a consistent speed for every frame rate
            transform.position += (Vector3.left * logic.moveSpeed) * Time.deltaTime;

            // If the ground go too far in the X position past the "deadZone" the ground object will be destroyed
            if (transform.position.x < deadZone)
            {
                Destroy(gameObject);
            }
        }
    }
}