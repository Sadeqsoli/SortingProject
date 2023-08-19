using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeapSorting : MonoBehaviour, ISort
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
        return HeapSort(thaList, thaList.Length);
    }
    int[] HeapSort(int[] array, int size)
    {
        try
        {
            if (size <= 1)
                return array;
            for (int i = size / 2 - 1; i >= 0; i--)
            {
                Heapify(array, size, i);
            }
            for (int i = size - 1; i >= 0; i--)
            {
                var tempVar = array[0];
                array[0] = array[i];
                array[i] = tempVar;
                Heapify(array, i, 0);
            }
        }
        catch (StackOverflowException e)
        {
            EventManager.ReportSenderEvent?.Invoke(ReportType.Error, "SOF: Too many, use another sorting algorithm!");
            Debug.Log("Error: " + e.Message);
        }
        catch (Exception e)
        {
            EventManager.ReportSenderEvent?.Invoke(ReportType.Error, "Unknown: " + e.Message);
            Debug.Log("Error: " + e.Message);
        }
        return array;
    }
    void Heapify(int[] array, int size, int index)
    {
        var largestIndex = index;
        var leftChild = 2 * index + 1;
        var rightChild = 2 * index + 2;
        if (leftChild < size && array[leftChild] > array[largestIndex])
        {
            largestIndex = leftChild;
        }
        if (rightChild < size && array[rightChild] > array[largestIndex])
        {
            largestIndex = rightChild;
        }
        if (largestIndex != index)
        {
            var tempVar = array[index];
            array[index] = array[largestIndex];
            array[largestIndex] = tempVar;
            Heapify(array, size, largestIndex);
        }
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
