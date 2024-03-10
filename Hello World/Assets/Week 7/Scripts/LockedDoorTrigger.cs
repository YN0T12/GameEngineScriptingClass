using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LockedDoorTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject doorObject;
    
    bool unlocked = false;

    [SerializeField]
    float doorSpeed = 2f;

    [SerializeField]
    float doorHeight = 5f;

    Vector3 origin;
    Vector3 target;

    private void Awake()
    {
        origin = doorObject.transform.position;
        target = new Vector3(origin.x, origin.y + doorHeight, origin.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (unlocked)
        {
            doorObject.transform.DOMove(target, doorSpeed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (unlocked)
        {
            doorObject.transform.DOMove(origin, doorSpeed);
        }
    }

    public void UnlockDoor()
    {
        unlocked = true;
    }
}
