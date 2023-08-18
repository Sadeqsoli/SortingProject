using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] Button AddNumbersButton;
    [SerializeField] Transform _mSelectionsParent;
    [SerializeField] GameObject _mSortingPanel;




    private void OnEnable()
    {
        AddNumbersButton.interactable = false;
        EventManager.NumberSelectionEvent?.AddListener(GetNumberSelection);
        EventManager.SelectionSenderEvent?.AddListener(ActivateChosenSelection);
    }

    private void GetNumberSelection(NumbSelection selection)
    {
        var listOfRandomNumbers = MathGen.GenerateRandom(selection.Count, selection.Min, selection.Max).ToArray();
        AddNumbersButton.onClick.RemoveAllListeners();
        AddNumbersButton.onClick.AddListener(() => SendingGeneratedRandomNumbers(listOfRandomNumbers));
    }

    void SendingGeneratedRandomNumbers(int[] ListOfUnsortedNumbers)
    {
        _mSortingPanel.SetActive(true);
        EventManager.ListSenderEvent?.Invoke(ListOfUnsortedNumbers);
    }

    private void OnDisable()
    {
        EventManager.NumberSelectionEvent?.RemoveListener(GetNumberSelection);
        EventManager.SelectionSenderEvent?.RemoveListener(ActivateChosenSelection);
    }


    void ActivateChosenSelection(ISelection selection)
    {
        var targetSelection = (int)selection.GetSelectionType();
        var selectionTypeLength = _mSelectionsParent.childCount;
        for (int i = 0; i < selectionTypeLength; i++)
        {
            if (i == targetSelection)
            {
                if (selection.SelectingNumbers())
                    AddNumbersButton.interactable = true;
                else
                    AddNumbersButton.interactable = false;

            }
            else
            {
                _mSelectionsParent.GetChild(i).GetComponent<ISelection>().ResetSelection();
            }
        }
    }

}//endClasss
