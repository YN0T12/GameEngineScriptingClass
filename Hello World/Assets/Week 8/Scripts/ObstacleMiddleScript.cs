using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week8
{
    public class ObstacleMiddleScript : MonoBehaviour
    {
        [SerializeField] LogicScript logic;
        [SerializeField] BirdScript bird;

        // Start is called before the first frame update
        void Start()
        {
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
            bird = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (bird.birdIsAlive)
            {
                Debug.Log("Score!");
                logic.addScore(1);
            }
        }
    }
}
