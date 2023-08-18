using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InsertionSorting : MonoBehaviour, ISort
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
