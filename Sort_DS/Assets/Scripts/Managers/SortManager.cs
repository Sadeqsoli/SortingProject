using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class SortManager : MonoBehaviour
{
    [SerializeField] Numb NumberPrefab;
    [SerializeField] Transform ParentTR;
    [SerializeField] Transform _mSortParent;
    [SerializeField] TextMeshProUGUI SortTypeTXT;
    [SerializeField] TextMeshProUGUI SortReportTXT;
    [SerializeField] TextMeshProUGUI DisReportTXT;
    [SerializeField] TextMeshProUGUI StatisticsTXT;
    NumbSelection numbs;
    int[] _mCurrentSelectedList;

    private void Awake()
    {
        EventManager.NumberSelectionEvent?.AddListener(GetSelectionData);
        EventManager.ListSenderEvent?.AddListener(InitializeSortDisplay);
        EventManager.SortSenderEvent?.AddListener(ActivateChosenSort);
        EventManager.ReportSenderEvent?.AddListener(SetNewReport);
    }

    void GetSelectionData(NumbSelection _selection)
    {
        numbs = _selection;
    }
    void GetStatistics()
    {
        var statistics = numbs.Count + " numbers from " + numbs.Min + " To " + numbs.Max;
        StatisticsTXT.text = statistics;
        SortReportTXT.text = "Test different sort algorithms with different selections to know which situation is fit your need!";
        SortTypeTXT.text = "Sort Type";
    }

    void SetNewReport(ReportType reportType,string report)
    {
        SortReportTXT.text = report;
        Debug.Log(reportType + ": " + report);
    }

    void InitializeSortDisplay(int[] listOfNumbers)
    {
        GetStatistics();
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
            var trSorterChild = _mSortParent.GetChild(i);
            var trSorterChildLength = _mSortParent.GetChild(i).childCount;
            for (int j = 0; j < trSorterChildLength; j++)
            {
                if (trSorterChild.GetChild(j).GetComponent<ISort>().GetSortType() == targetSelection)
                {
                    var watchSortAlgorithm = System.Diagnostics.Stopwatch.StartNew();
                    var sortedArray = sorter.Sort(_mCurrentSelectedList);

                    watchSortAlgorithm.Stop();


                    var sortMess =  "Sorted: " + watchSortAlgorithm.Elapsed.TotalSeconds + "(s)";
                    SortTypeTXT.text = targetSelection.ToString();
                    SetNewReport(ReportType.Success, sortMess);
                    //Debug.Log("Sort-Milis: " + watchSortAlgorithm.ElapsedMilliseconds);
                    //Debug.Log("Sort-TimeSpan: " + watchSortAlgorithm.Elapsed);
                    //Debug.Log("Sort-Sec: " + watchSortAlgorithm.Elapsed.Seconds);

                    if (sortedArray != null)
                    {
                    var watchDisAlgorithm = System.Diagnostics.Stopwatch.StartNew();
                        UpdateSortDisplay(sortedArray);
                        watchDisAlgorithm.Stop();

                        //Debug.Log("Dis-Milis: " + watchDisAlgorithm.ElapsedMilliseconds);
                        //Debug.Log("Dis-TimeSpan: " + watchDisAlgorithm.Elapsed);
                        //Debug.Log("Dis-Sec: " + watchDisAlgorithm.Elapsed.Seconds);
                        var disMess = "Displayed: " + watchDisAlgorithm.Elapsed.TotalSeconds + "(s)";

                        DisReportTXT.text = disMess;
                    }
                }
                else
                {
                    trSorterChild.GetChild(j).GetComponent<ISort>().Deselect();
                }
            }
        }
    }

    private void OnDisable()
    {
        EventManager.ListSenderEvent?.RemoveListener(InitializeSortDisplay);
        EventManager.SortSenderEvent?.RemoveListener(ActivateChosenSort);
        EventManager.ReportSenderEvent?.AddListener(SetNewReport);
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

