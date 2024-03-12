using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week8
{
    public class ObstacleSpawnerScript : MonoBehaviour
    {
        [SerializeField] LogicScript logic;
        float spawnRate = 2;
        float timer = 2;

        [SerializeField] GameObject pipeObject;

        // Update is called once per frame
        void Update()
        {
            if (logic.startGameCheck == true)
            {
                if (timer < spawnRate)
                {
                    timer = timer + Time.deltaTime;
                }
                else
                {
                    spawnObstacle(pipeObject, -3, 8, 3.5f);
                    timer = 0;
                }
            }
        }

        void spawnObstacle(GameObject Obstacle, float lowestPoint, float highestPoint, float spawnDelay)
        {
            Vector3 SpawnPosition = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0);
            spawnRate = spawnDelay;

            //spawn obstacle
            Instantiate(Obstacle, SpawnPosition, transform.rotation);
        }
    }
}