using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSorting : MonoBehaviour, ISort
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
        try
        {
            var listLength = thaList.Length;
            for (int i = 0; i < listLength - 1; i++)
            {
                var min = i;
                for (int j = i + 1; j < listLength; j++)
                {
                    if (thaList[j] < thaList[min])
                    {
                        min = j;
                    }
                }
                var tempVar = thaList[min];
                thaList[min] = thaList[i];
                thaList[i] = tempVar;
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
