using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayDemo : MonoBehaviour
{
    [SerializeField]
    public int[][] scoresArray = new int[4][];

    public int[,] scoresArray2 = new int[3,3];


    [ContextMenu("Execute Test")]
    void Execute()
    {

        for (int i = 0; i < scoresArray.Length; i++) 
        {
            Debug.LogFormat("the number is... {0} tadumus!", scoresArray[i]);
        }
    }

}
