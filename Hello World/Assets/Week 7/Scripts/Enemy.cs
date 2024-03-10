using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Week7
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private float health = 100;

        [SerializeField]
        private Material normalMat;
        [SerializeField]
        private Material damagedMat;

        private MeshRenderer mesh;

        private void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
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
                    Destroy(gameObject, 0.1f);
                }
            }
        }
    }
}

