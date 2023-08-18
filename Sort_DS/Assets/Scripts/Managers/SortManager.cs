using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class SortManager : MonoBehaviour
{
    [SerializeField] Numb NumberPrefab;
    [SerializeField] Transform ParentTR;
    [SerializeField] Transform _mSortParent;

    int[] _mCurrentSelectedList;

    private void Awake()
    {
        EventManager.ListSenderEvent?.AddListener(InitializeSortDisplay);
        EventManager.SortSenderEvent?.AddListener(ActivateChosenSort);
    }

    private void InitializeSortDisplay(int[] listOfNumbers)
    {
        ResetSortPanel();
        _mCurrentSelectedList = listOfNumbers;

        foreach (var number in listOfNumbers)
        {
            var numbInstance = Instantiate(NumberPrefab);
            numbInstance.transform.SetParent(ParentTR);
            numbInstance.transform.localPosition = Vector3.zero;
            numbInstance.transform.localScale = Vector3.one;
            numbInstance.InitNumber(number);
        }

    }
    private void UpdateSortDisplay(int[] listOfNumbers)
    {
        ResetSortPanel();

        foreach (var number in listOfNumbers)
        {
            var numbInstance = Instantiate(NumberPrefab);
            numbInstance.transform.SetParent(ParentTR);
            numbInstance.transform.localPosition = Vector3.zero;
            numbInstance.transform.localScale = Vector3.one;
            numbInstance.InitNumber(number);
        }

    }

    void ActivateChosenSort(ISort sorter)
    {
        var targetSelection = sorter.GetSortType();
        var selectionTypeLength = _mSortParent.childCount;
        for (int i = 0; i < selectionTypeLength; i++)
        {
            var length = _mSortParent.childCount;
            for (int j = 0; j < length; j++)
            {
                var trSorterChild = _mSortParent.GetChild(j);
                var trSorterChildLength = _mSortParent.GetChild(j).childCount;
                for (int k = 0; k < trSorterChildLength; k++)
                {
                    if (trSorterChild.GetChild(k).GetComponent<ISort>().GetSortType() == targetSelection)
                    {
                        var sortedArray = sorter.Sort(_mCurrentSelectedList);
                        if (sortedArray != null)
                        {
                            UpdateSortDisplay(sortedArray);
                        }
                    }
                    else
                    {
                        trSorterChild.GetChild(k).GetComponent<ISort>().Deselect();
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        EventManager.ListSenderEvent?.RemoveListener(InitializeSortDisplay);
        EventManager.SortSenderEvent?.RemoveListener(ActivateChosenSort);
        ResetSortPanel();
    }

    void SetOnOff(bool isOn)
    {
        var parentTrChildCount = ParentTR.childCount;
        if (parentTrChildCount > 0)
            for (int i = 0; i < parentTrChildCount; i++)
            {
                ParentTR.GetChild(i).gameObject.SetActive(isOn);
            }
    }
    void ResetSortPanel()
    {
        DestroyChildren();
    }
    void DestroyChildren()
    {
        var parentTrChildCount = ParentTR.childCount;
        if (parentTrChildCount > 0)
            for (int i = 0; i < parentTrChildCount; i++)
            {
                Destroy(ParentTR.GetChild(i).gameObject);
            }
    }

}

