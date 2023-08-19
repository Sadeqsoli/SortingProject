using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionZeroToInput : MonoBehaviour, ISelection
{
    [SerializeField] TMP_InputField countInput;
    public SelectionType selectionType = SelectionType.ZeroToInput;
    [SerializeField] TMP_InputField maxInput;
    [SerializeField] TextMeshProUGUI WarningTXT;
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
        if (string.IsNullOrEmpty(countInput.text))
        {
            LogWarnings("Input for count number is Empty!", _Color.R_DRed);
            countInput.targetGraphic.color = _Color.R_SLRed;
            countInput.Select();
            return false;
        }
        short countNumber = -1;
        if (!short.TryParse(this.countInput.text, out countNumber))
        {
            LogWarnings("Couldn't parse Count Input Number, Try again!", _Color.R_DRed);
            countInput.targetGraphic.color = _Color.R_SLRed;
            return false;
        }
        if (countNumber < 100)
        {
            LogWarnings("write atleast 100 for the sake of this test!", _Color.R_DRed);
            countInput.targetGraphic.color = _Color.R_SLRed;
            return false;
        }

        if (string.IsNullOrEmpty(maxInput.text))
        {
            LogWarnings("Input for max number is Empty!", _Color.R_DRed);
            maxInput.targetGraphic.color = _Color.R_SLRed;
            maxInput.Select();
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
        if (countNumber > difference)
        {
            LogWarnings("Count number is bigger than the chosen domain!", _Color.R_DRed);
            countInput.targetGraphic.color = _Color.R_SLRed;
            countInput.Select();
            return false;
        }

        var zeroToInput = new NumbSelection(countNumber, minNumb, maxNumb);
        if (zeroToInput != null)
        {
            EventManager.NumberSelectionEvent?.Invoke(zeroToInput);
        }
        else
        {
            LogWarnings("Number Selction is Null!", _Color.R_DRed);
            return false;
        }
        Debug.Log("difference: " + difference);
        Debug.Log("Count: " + zeroToInput.Count);
        Debug.Log("Min: " + zeroToInput.Min);
        Debug.Log("Max: " + zeroToInput.Max);
        maxInput.targetGraphic.color = _Color.G_SSLGreen;
        countInput.targetGraphic.color = _Color.G_SLGreen;

        LogWarnings("We have the right selection!", Color.black);
        ChoosingSelctionIMG.color = _Color.Y_LOlive;
        return true;
    }

    void LogWarnings(string warning, Color color)
    {
        WarningTXT.text = warning;
        WarningTXT.color = color;
        ChoosingSelctionIMG.color = _Color.WB_SLGray;
    }

    public void ResetSelection()
    {
        WarningTXT.text = "";
        ChoosingSelctionIMG.color = _Color.WB_SLGray;
        maxInput.targetGraphic.color = _Color.BaseWhite;
        countInput.targetGraphic.color = _Color.BaseWhite;
        maxInput.text = "";
        minNumb = 0;
        maxNumb = 1230;
    }
    public SelectionType GetSelectionType()
    {
        return selectionType;
    }

}//EndClasss
