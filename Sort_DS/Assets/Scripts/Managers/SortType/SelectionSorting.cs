using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSorting : MonoBehaviour, ISort
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
        var listLength = thaList.Length;
        for (int i = 0; i < listLength -1; i++)
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
        sendingSortType.image.color = _Color.Y_Olive;
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
