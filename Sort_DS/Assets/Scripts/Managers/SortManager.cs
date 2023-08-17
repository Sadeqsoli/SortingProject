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

    List<int> _mCurrentSelectedList;

    private void Awake()
    {
        EventManager.ListSenderEvent?.AddListener(InitializeSettingUp);
    }

    private void InitializeSettingUp(List<int> listOfNumbers)
    {
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
    private void OnDisable()
    {
        EventManager.ListSenderEvent?.RemoveListener(InitializeSettingUp);
    }

}

