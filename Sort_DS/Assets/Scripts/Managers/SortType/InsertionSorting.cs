using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertionSorting : MonoBehaviour, ISort
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
        var length = thaList.Length;
        for (int i = 1; i < length; i++)
        {
            var key = thaList[i];
            var flag = 0;
            for (int j = i - 1; j >= 0 && flag != 1;)
            {
                if (key < thaList[j])
                {
                    thaList[j + 1] = thaList[j];
                    j--;
                    thaList[j + 1] = key;
                }
                else flag = 1;
            }
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
