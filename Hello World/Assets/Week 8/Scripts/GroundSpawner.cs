using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Week8
{
    public class GroundSpawner : MonoBehaviour
    {
        [SerializeField] float spawnRate = 0;
        float timer = 0;
        [SerializeField] float height = 0;
        [SerializeField] GameObject ground;

        void Update()
        {
            // How to make a timer in Unity
            if (timer < spawnRate)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                spawnGround();
                timer = 0;
            }

        }

        // This will spawn a pipe every time the timer reaches "spawnRate" with a random offset in the Y position based on "heightOffset"
        void spawnGround()
        {
            Instantiate(ground, new Vector3(transform.position.x, height, 0), transform.rotation);
        }
    }
}
