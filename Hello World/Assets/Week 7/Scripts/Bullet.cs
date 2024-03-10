using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Week7;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 1;

    MeshRenderer mesh;
    SphereCollider collider;

    ParticleSystem burst;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        burst = GetComponent<ParticleSystem>();
        collider = GetComponent<SphereCollider>();

        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        mesh.enabled = false;
        burst.Play();
        Destroy(gameObject, 1f);
    }
}
