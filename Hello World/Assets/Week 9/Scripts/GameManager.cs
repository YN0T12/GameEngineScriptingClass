using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] public UnityEvent GameOverEvent;

    [ContextMenu("Do Test GameOverEvent")]
    private void TestGameOverEvent()
    {
        GameOverEvent.Invoke();
    }
}
