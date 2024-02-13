using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week4
{
    public class Enemy : MonoBehaviour
    {
        public int Health = 10;

        [SerializeField] private Player target;

        public void Damage(int amount)
        {
            Health -= amount;
        }

        void Attack()
        {
            target.Damage(3);
        }
    }
}

