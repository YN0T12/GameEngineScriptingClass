using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week8
{
    public class ObstacleMoveScript : MonoBehaviour
    {
        float deadZone;
        [SerializeField] LogicScript logic;

        // Start is called before the first frame update
        void Start()
        {
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += (Vector3.left * logic.moveSpeed) * Time.deltaTime;

            if (logic.startGameCheck == true)
            {
                deadZone = -50;
            }
            else if (logic.startGameCheck == false)
            {
                //deadZone = 30;
            }

            if (transform.position.x < deadZone)
            {
                Debug.Log("Obstacle Deleted");
                Destroy(gameObject);
            }
        }
    }
}
