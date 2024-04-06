using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Week7
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private float health = 5;
        private float currentHealth;

        [SerializeField]
        private Material normalMat;
        [SerializeField]
        private Material damagedMat;

        private MeshRenderer mesh;

        private void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
            currentHealth = health;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                mesh.material = damagedMat;
                health--;

                DOVirtual.DelayedCall(0.1f, () =>
                {
                    mesh.material = normalMat;
                });

                if (health <= 0)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
        public void RestartGame()
        {
            health = currentHealth;
            this.gameObject.SetActive(true);
        }
    }
}

