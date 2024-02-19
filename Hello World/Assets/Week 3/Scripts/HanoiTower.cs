using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HanoiTower : MonoBehaviour
{
    [SerializeField] private GameObject peg1;
    [SerializeField] private GameObject peg2;
    [SerializeField] private GameObject peg3;

    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject youWinMessage;

    [SerializeField] private int[] peg1Data = { 1, 2, 3, 4 };
    [SerializeField] private int[] peg2Data = { 0, 0, 0, 0 };
    [SerializeField] private int[] peg3Data = { 0, 0, 0, 0 };

    [SerializeField] private int currentPeg = 1;

    [ContextMenu("Move Right")]
    public void MoveRight()
    {
        //Make sure we aren't the right most peg
        if (CanMoveRight() == false) return;

        //Check to see what index and number we are moving from THIS peg
        int[] fromArray = GetPeg(currentPeg);
        int fromIndex = GetTopNumberIndex(fromArray);

        //If there wasn't anything to move then don't try to move
        if (fromIndex == -1) return;

        //Check to see where in the peg we are moving to that the number
        //should be placed into
        int[] toArray = GetPeg(currentPeg + 1);
        int toIndex = GetIndexOfFreeSlot(toArray);

        //If the adjacent peg is FULL then we cannot move anything into it
        //This probably will never happen since the max number of numbers
        //we have is the size of each peg
        if (toIndex == -1) return;

        //Lastly check to verify the number we are moving is not larger
        //than whatever number we may be placing this number on top of
        //on the adjacent peg
        if (CanAddToPeg(fromArray[fromIndex], toArray) == false) return;

        //If all checks PASS then go aheand and move the number
        //out of THIS array into the adjacent array
        MoveNumber(fromArray, fromIndex, toArray, toIndex);

        Transform disc = PopDiscFromCurrentPeg();
        Transform toPeg = GetPegTransform(currentPeg + 1);

        disc.SetParent(toPeg);

        IncrementPegNumber();

        CheckForWin();
    }

    [ContextMenu("Move Left")]
    public void MoveLeft()
    {
        //Make sure we aren't the left most peg
        if (CanMoveLeft() == false) return;

        //Check to see what index and number we are moving from THIS peg
        int[] fromArray = GetPeg(currentPeg);
        int fromIndex = GetTopNumberIndex(fromArray);

        //If there wasn't anything to move then don't try to move
        if (fromIndex == -1) return;

        //Check to see where in the peg we are moving to that the number
        //should be placed into
        int[] toArray = GetPeg(currentPeg - 1);
        int toIndex = GetIndexOfFreeSlot(toArray);

        //If the adjacent peg is FULL then we cannot move anything into it
        //This probably will never happen since the max number of numbers
        //we have is the size of each peg
        if (toIndex == -1) return;

        //Lastly check to verify the number we are moving is not larger
        //than whatever number we may be placing this number on top of
        //on the adjacent peg
        if (CanAddToPeg(fromArray[fromIndex], toArray) == false) return;

        //If all checks PASS then go aheand and move the number
        //out of THIS array into the adjacent array
        MoveNumber(fromArray, fromIndex, toArray, toIndex);

        Transform disc = PopDiscFromCurrentPeg();
        Transform toPeg = GetPegTransform(currentPeg - 1);

        disc.SetParent(toPeg);

        DecrementPegNumber();

        CheckForWin();
    }

    Transform PopDiscFromCurrentPeg()
    {
        Transform currentPegTransform = GetPegTransform(currentPeg);

        int index = currentPegTransform.childCount - 1;
        Transform disk = currentPegTransform.GetChild(index);
        return disk;
    }

    Transform GetPegTransform(int pegNumber)
    {
        //Alternative way to find our objects
        //return GameObject.Find($"Peg-{pegNumber}").transform;

        if (pegNumber == 1) return peg1.transform;

        if (pegNumber == 2) return peg2.transform;

        return peg3.transform;
    }

    public void IncrementPegNumber()
    {
        currentPeg++;
        if (currentPeg == 1) { ChangePegColor(peg1); }
        if (currentPeg == 2) { ChangePegColor(peg2); }
        if (currentPeg == 3) { ChangePegColor(peg3); }
        if (currentPeg > 3) { DecrementPegNumber(); }
    }

    public void DecrementPegNumber()
    {
        currentPeg--;
        if (currentPeg == 1) { ChangePegColor(peg1); }
        if (currentPeg == 2) { ChangePegColor(peg2); }
        if (currentPeg == 3) { ChangePegColor(peg3); }
        if (currentPeg < 1) { IncrementPegNumber(); }
    }

    void MoveNumber(int[] fromArr, int fromIndex, int[] toArr, int toIndex)
    {
        int value = fromArr[fromIndex];
        fromArr[fromIndex] = 0;

        toArr[toIndex] = value;
    }

    bool CanMoveRight()
    {
        //If peg 1 or 2 then can move right
        return currentPeg < 3;
    }

    bool CanAddToPeg(int value, int[] peg)
    {
        int topNumberIndex = GetTopNumberIndex(peg);
        if(topNumberIndex == -1) return true;

        int topNumber = peg[topNumberIndex];
        return topNumber > value;
    }

    bool CanMoveLeft()
    {
        //If peg 2 or 3 then can move right
        return currentPeg > 1;
    }

    int[] GetPeg(int pegNumber)
    {
        if (pegNumber == 1) return peg1Data;

        if (pegNumber == 2) return peg2Data;

        return peg3Data;
    }

    int GetTopNumberIndex(int[] peg)
    {
        for (int i = 0; i < peg.Length; i++)
        {
            if (peg[i] != 0) return i;
        }

        return -1;
    }

    int GetIndexOfFreeSlot(int[] peg)
    {
        for (int i = peg.Length - 1; i >= 0; i--)
        {
            if (peg[i] == 0) return i;
        }

        return -1;
    }

    void ChangePegColor(GameObject selectedPeg)
    {
        peg1.GetComponent<Image>().color = new Color32(125, 84, 54, 225);
        peg2.GetComponent<Image>().color = new Color32(125, 84, 54, 225);
        peg3.GetComponent<Image>().color = new Color32(125, 84, 54, 225);

        selectedPeg.GetComponent<Image>().color = new Color32(195, 138, 52, 225);
    }

    void CheckForWin()
    {
        if (peg3Data.SequenceEqual(new int[] { 1, 2, 3, 4 }))
        {
            peg1.GetComponent<Image>().color = new Color32(125, 84, 54, 225);
            peg2.GetComponent<Image>().color = new Color32(125, 84, 54, 225);
            peg3.GetComponent<Image>().color = new Color32(125, 84, 54, 225);

            buttons.SetActive(false);
            youWinMessage.SetActive(true);
        }
    }
}
