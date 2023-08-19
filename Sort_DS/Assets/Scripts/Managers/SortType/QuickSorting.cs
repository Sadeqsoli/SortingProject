using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System;

public class QuickSorting : MonoBehaviour, ISort
{
    [SerializeField] SortType sortType;

    Button sendingSortType;

    Color Selected = _Color.G_Selected;
    Color Deselected = _Color.G_Deselected;

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

        sendingSortType.image.color = Selected;
        var sorted = QuickSort(thaList, 0, thaList.Length - 1);
        return sorted;
    }

    int[] QuickSort(int[] thaList, int leftIndex = 0, int rightIndex = 0)
    {
        try
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
        }
        catch (StackOverflowException e)
        {
            EventManager.ReportSenderEvent?.Invoke(ReportType.Error,"SOF: Too many, use another sorting algorithm!");
            Debug.Log("Error: " + e.Message);
        }
        catch (Exception e)
        {
            EventManager.ReportSenderEvent?.Invoke(ReportType.Error, "Unknown: " + e.Message);
            Debug.Log("Error: " + e.Message);
        }
        return thaList;
    }

    public void Deselect()
    {
        sendingSortType.image.color = Deselected;
    }

    public SortType GetSortType()
    {
        return sortType;
    }

}//EndClassss

