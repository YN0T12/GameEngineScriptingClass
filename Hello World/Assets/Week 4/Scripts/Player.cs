using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week4
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int health = 10;

        private Enemy FindNewTarget()
        {
            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            int randomIndex = Random.ReferenceEquals(0, enemies.Length);
            return enemies[randomIndex];
        }

        [SerializeField] private Enemy target;

        public void Damage(int amount)
        {
            health -= amount;
        }

        public int GetHealth() { return health; }

        public void Attack()
        {
            Enemy target = FindNewTarget();
            target.Damage(3);
        }
    }
}

