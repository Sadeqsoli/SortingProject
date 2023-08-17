using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionZeroToInput : MonoBehaviour, ISelection
{
    public SelectionType selectionType = SelectionType.ZeroToInput;
    [SerializeField] TMP_InputField maxInput;
    [SerializeField] TextMeshProUGUI WarningTXT;
    [SerializeField] short _mCountSelection = 100;
    short minNumb = 0;
    short maxNumb = -1;
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
        if (string.IsNullOrEmpty(maxInput.text))
        {
            LogWarnings("Input for max number is Empty!", _Color.R_DRed);
            maxInput.targetGraphic.color = _Color.R_SLRed;
            return false;
        }
        short maxNumb = -1;
        if (!short.TryParse(this.maxInput.text, out maxNumb))
        {
            LogWarnings("Couldn't parse Max Input Number, Try again!", _Color.R_DRed);
            maxInput.targetGraphic.color = _Color.R_SLRed;
            return false;
        }

        var difference = maxNumb - minNumb;

        if (difference >= 100 || difference <= -100)
        {
            var zeroToInput = new NumbSelection(_mCountSelection, minNumb, maxNumb);
            if (zeroToInput != null)
            {
                EventManager.NumberSelectionEvent?.Invoke(zeroToInput);
            }
            else
            {
                Debug.Log("NumbSelction is Null!");
                return false;
            }
            Debug.Log("difference: " + difference);
            Debug.Log("Count: " + zeroToInput.Count);
            Debug.Log("Min: " + zeroToInput.Min);
            Debug.Log("Max: " + zeroToInput.Max);
            maxInput.targetGraphic.color = _Color.G_SSLGreen;
            LogWarnings("We have the right selection!", Color.black);
            ChoosingSelctionIMG.color = _Color.Y_LOlive;
            return true;
        }
        else
        {
            LogWarnings("At-least 100 between Min and Max numbers", _Color.R_DRed);
            maxInput.targetGraphic.color = _Color.R_SLRed;
            return false;
        }
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
        maxInput.targetGraphic.color = _Color.BaseWhite;
        maxInput.text = "";
        minNumb = 0;
        maxNumb = _mCountSelection;
    }
    public SelectionType GetSelectionType()
    {
        return selectionType;
    }

}//EndClasss
