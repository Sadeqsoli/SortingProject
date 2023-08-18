using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeapSorting : MonoBehaviour, ISort
{
    [SerializeField] SortType sortType;

    Button sendingSortType;

    Color Selected = _Color.G_SelectedGreen;
    Color Deselected = _Color.G_DeselectedGreen;
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
        var watch = System.Diagnostics.Stopwatch.StartNew();
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
        watch.Stop();
        Timer.passedTime = watch.ElapsedMilliseconds;
        Debug.Log("MiliS: " + watch.ElapsedMilliseconds);
        Debug.Log("Ticks: " + watch.ElapsedTicks);
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
