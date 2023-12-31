using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadixSorting : MonoBehaviour, ISort
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
        return RadixSort(thaList, thaList.Length);
    }
    public int[] RadixSort(int[] array, int size)
    {
        try
        {
            var maxVal = GetMaxVal(array, size);
            for (int exponent = 1; maxVal / exponent > 0; exponent *= 10)
                CountingSort(array, size, exponent);
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
    public static int GetMaxVal(int[] array, int size)
    {
        var maxVal = array[0];
        for (int i = 1; i < size; i++)
            if (array[i] > maxVal)
                maxVal = array[i];
        return maxVal;
    }
    void CountingSort(int[] array, int size, int exponent)
    {
        var outputArr = new int[size];
        var occurences = new int[10];
        for (int i = 0; i < 10; i++)
            occurences[i] = 0;
        for (int i = 0; i < size; i++)
            occurences[(array[i] / exponent) % 10]++;
        for (int i = 1; i < 10; i++)
            occurences[i] += occurences[i - 1];
        for (int i = size - 1; i >= 0; i--)
        {
            outputArr[occurences[(array[i] / exponent) % 10] - 1] = array[i];
            occurences[(array[i] / exponent) % 10]--;
        }
        for (int i = 0; i < size; i++)
            array[i] = outputArr[i];
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
