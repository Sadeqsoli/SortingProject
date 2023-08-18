using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeSorting : MonoBehaviour, ISort
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
    
    int[] MergeArray(int[] thaList, int left, int middle, int right)
    {
        var leftArrayLength = middle - left + 1;
        var rightArrayLength = right - middle;
        var leftTempArray = new int[leftArrayLength];
        var rightTempArray = new int[rightArrayLength];
        int i, j;
        for (i = 0; i < leftArrayLength; ++i)
            leftTempArray[i] = thaList[left + i];
        for (j = 0; j < rightArrayLength; ++j)
            rightTempArray[j] = thaList[middle + 1 + j];
        i = 0;
        j = 0;
        int k = left;
        while (i < leftArrayLength && j < rightArrayLength)
        {
            if (leftTempArray[i] <= rightTempArray[j])
            {
                thaList[k++] = leftTempArray[i++];
            }
            else
            {
                thaList[k++] = rightTempArray[j++];
            }
        }
        while (i < leftArrayLength)
        {
            thaList[k++] = leftTempArray[i++];
        }
        while (j < rightArrayLength)
        {
            thaList[k++] = rightTempArray[j++];
        }
        return thaList;
    }
    int[] MergeSort(int[] thaList, int left, int right)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        if (left < right)
        {
            int middle = left + (right - left) / 2;

            MergeSort(thaList, left, middle);
            MergeSort(thaList, middle + 1, right);

            thaList = MergeArray(thaList, left, middle, right);
        }
        watch.Stop();
        Timer.passedTime = watch.ElapsedMilliseconds;
        Debug.Log("MiliS: " + watch.ElapsedMilliseconds);
        Debug.Log("Ticks: " + watch.ElapsedTicks);
        return thaList;
    }
    public int[] Sort(int[] thaList)
    {
        thaList = MergeSort(thaList, 0, thaList.Length - 1);
        sendingSortType.image.color = Selected;
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