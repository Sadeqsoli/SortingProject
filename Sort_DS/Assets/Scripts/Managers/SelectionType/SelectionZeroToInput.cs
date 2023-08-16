using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionZeroToInput : MonoBehaviour
{
    [SerializeField] TMP_InputField maxInput;
    [SerializeField] short _mCountSelection = 100;
    short minNumb = 0;
    short maxNumb = 0;
    Button ChoosingSelction;
    void Start()
    {
        ChoosingSelction.onClick.AddListener(SelectingNumbers);
    }


    void SelectingNumbers()
    {
        NumbSelection zeroToInput = new NumbSelection();
        short maxIn = -1;
        if (!short.TryParse(maxInput.text, out maxIn))
        {
            Debug.Log("Couldn't parse it, Try again!");
            return;
        }
        zeroToInput.selectionCount = _mCountSelection;
        zeroToInput.minSelection = minNumb;
        zeroToInput.maxSelection = maxNumb;
    }
}
