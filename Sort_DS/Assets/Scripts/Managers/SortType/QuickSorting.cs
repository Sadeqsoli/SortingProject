using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class QuickSorting : MonoBehaviour, ISort
{
    [SerializeField] SortType sortType;

    Button sendingSortType;

    private void Start()
    {
        sendingSortType = GetComponent<Button>();
        sendingSortType.onClick.AddListener(SendingCurrentSorting);
        Deselect();
    }


    void SendingCurrentSorting()
    {
        EventManager.SortSenderEvent?.Invoke(this);
    }

    public int[] Sort(int[] thaList)
    {
        sendingSortType.image.color = _Color.Y_Olive;
        return QuickSort(thaList, 0, thaList.Length - 1);
    }

    int[] QuickSort(int[] thaList, int leftIndex = 0, int rightIndex = 0)
    {
        var i = leftIndex;
        var j = rightIndex;
        var pivot = thaList[leftIndex];

        while (i <= j)
        {
            while (thaList[i] < pivot)
            {
                i++;
            }

            while (thaList[j] > pivot)
            {
                j--;
            }
            if (i <= j)
            {
                int temp = thaList[i];
                thaList[i] = thaList[j];
                thaList[j] = temp;
                i++;
                j--;
            }
        }

        if (leftIndex < j)
            QuickSort(thaList, leftIndex, j);
        if (i < rightIndex)
            QuickSort(thaList, i, rightIndex);
        return thaList;
    }

    public void Deselect()
    {
        sendingSortType.image.color = _Color.Y_LOlive;
    }

    public SortType GetSortType()
    {
        return sortType;
    }

}//EndClassss
