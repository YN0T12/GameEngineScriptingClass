using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpGame : MonoBehaviour
{
    public bool FastForward = false;
    public float FastForwardAmnt = 2;

    void Update()
    {
        if (FastForward) { Time.timeScale = FastForwardAmnt; }
        else { Time.timeScale = 1; }
    }

    public void FastForwardButton()
    {
        if (FastForward) { FastForward = false; }
        else { FastForward = true; }
    }
}
