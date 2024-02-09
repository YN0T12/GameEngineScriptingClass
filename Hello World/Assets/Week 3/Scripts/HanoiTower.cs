using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiTower : MonoBehaviour
{
    //Requirements
    //1. Disks can only move 1 peg to left or right
    //2. Disks need to be on 3rd peg where largest is on the bottom and smallest on top
    //3. Cannot move larger disks on top of smaller disks
    //4. Cannot move larger disk onto smaller disk

    [SerializeField]
    int[] peg1 = { 1, 2, 3, 4, 5, 6, 7, 8 };
    [SerializeField]
    int[] peg2 = { 0, 0, 0, 0, 0, 0, 0, 0 };
    [SerializeField]
    int[] peg3 = { 0, 0, 0, 0, 0, 0, 0, 0 };

    public int currentPeg = 1;
    int[] currentList;
    int[] targetList;

    [ContextMenu("Move Right")]
    void MoveRight()
    {
        int[] currentList = getPeg(currentPeg);
        int[] targetList = getPeg(currentPeg + 1);

        if (targetList == null) return;

        int fromIndex = getTopNumIndex(currentList);
        int toIndex = getBottomNumIndex(targetList);

        if (toIndex == -1) return;
        if (canMoveIntoPeg(currentList[fromIndex], currentList) == false) return;

        moveIntoPeg(fromIndex, toIndex, currentList, targetList);
    }

    [ContextMenu("Move Left")]
    void MoveLeft()
    {
        int[] currentList = getPeg(currentPeg);
        int[] tagetList = getPeg(currentPeg - 1);

        if (targetList == null) return;

        int fromIndex = getTopNumIndex(currentList);
        int toIndex = getBottomNumIndex(targetList);

        if (toIndex == -1) return;
        if (canMoveIntoPeg(currentList[fromIndex], currentList) == false) return;

        moveIntoPeg(fromIndex, toIndex, currentList, tagetList);
    }

    int getTopNumIndex(int[] peg)
    {
        for(int i = 0; i < peg.Length; i++) 
        {
            if (peg[i] != 0) return i;
        }

        return -1;
    }
    int getBottomNumIndex(int[] peg)
    {
        for (int i = peg.Length - 1; i >= 0; i--) 
        {
            if (peg[i] == 0) return i;
        }
        return -1;
    }

    bool canMoveIntoPeg(int numberToMove, int[] peg)
    {
        int bottomIndex = getBottomNumIndex(peg);

        //if (bottomIndex == peg.Length - 1 && peg[peg.Length - 1]) return true;

        int bottomPlus1 = bottomIndex++;
        return bottomPlus1 == 0;
    }

    void moveIntoPeg(int fromIndex, int toIndex, int[] from, int[] to)
    {
        int numToMove = from[fromIndex];
        from[fromIndex] = 0;
        to[toIndex] = numToMove;
    }

    int[] getPeg(int peg)
    {
        if (peg == 1) return peg1;
        if (peg == 2) return peg2;
        if (peg == 3) return peg3;
        return null;
    }
}
