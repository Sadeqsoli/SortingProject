using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionZeroToRandom : MonoBehaviour, ISelection
{
    public SelectionType selectionType = SelectionType.ZeroToRandom;
    [SerializeField] TextMeshProUGUI WarningTXT;
    [SerializeField] short _mCountSelection = 100;
    short minNumb = 0;
    short maxNumb = 5000;
    Button ChoosingSelctionBTN;
    Image ChoosingSelctionIMG;
    void OnEnable()
    {
        ChoosingSelctionBTN = GetComponent<Button>();
        ChoosingSelctionIMG = GetComponent<Image>();
        ResetSelection();
    }
    private void Start()
    {
        ChoosingSelctionBTN.onClick.AddListener(ChooseThisSelection);
    }

    void ChooseThisSelection()
    {
        EventManager.SelectionSenderEvent.Invoke(this);
    }
    public bool SelectingNumbers()
    {
        var zeroToRandom = new NumbSelection(_mCountSelection, minNumb, maxNumb);
        if (zeroToRandom != null)
        {
            EventManager.NumberSelectionEvent?.Invoke(zeroToRandom);
        }
        else
        {
            Debug.Log("NumbSelction is Null!");
            return false;
        }
        Debug.Log("Count: " + zeroToRandom.Count);
        Debug.Log("Min: " + zeroToRandom.Min);
        Debug.Log("Max: " + zeroToRandom.Max);
        ChoosingSelctionIMG.color = _Color.Y_LOlive;
        LogWarnings("We have the right selection!", Color.black);
        return true;
    }

    void LogWarnings(string warning, Color color)
    {
        WarningTXT.text = warning;
        WarningTXT.color = color;
    }


    public void ResetSelection()
    {
        WarningTXT.text = "";
        ChoosingSelctionIMG.color = _Color.WB_SLGray;
        minNumb = 0;
        maxNumb = 5000;
    }
    public SelectionType GetSelectionType()
    {
        return selectionType;
    }

}//EndClasss
