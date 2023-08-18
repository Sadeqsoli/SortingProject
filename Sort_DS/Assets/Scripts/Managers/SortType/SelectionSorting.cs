using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSorting : MonoBehaviour, ISort
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
        var watch = System.Diagnostics.Stopwatch.StartNew();
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
        watch.Stop();
        Timer.passedTime = watch.ElapsedMilliseconds;
        Debug.Log("MiliS: " + watch.ElapsedMilliseconds);
        Debug.Log("Ticks: " + watch.ElapsedTicks);
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
