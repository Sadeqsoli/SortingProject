using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionZeroToRandom : MonoBehaviour, ISelection
{
    [SerializeField] TMP_InputField countInput;
    public SelectionType selectionType = SelectionType.ZeroToRandom;
    [SerializeField] TextMeshProUGUI WarningTXT;
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
        var difference = maxNumb - minNumb;
        if (countNumber > difference)
        {
            LogWarnings("Count number is bigger than the chosen domain!", _Color.R_DRed);
            countInput.targetGraphic.color = _Color.R_SLRed;
            countInput.Select();
            return false;
        }
        var zeroToRandom = new NumbSelection(countNumber, minNumb, maxNumb);
        if (zeroToRandom != null)
        {
            EventManager.NumberSelectionEvent?.Invoke(zeroToRandom);
        }
        else
        {
            LogWarnings("Number Selction is Null!", _Color.R_DRed);
            return false;
        }
        Debug.Log("Count: " + zeroToRandom.Count);
        Debug.Log("Min: " + zeroToRandom.Min);
        Debug.Log("Max: " + zeroToRandom.Max);
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
        countInput.targetGraphic.color = _Color.BaseWhite;
        minNumb = 0;
        maxNumb = 5000;
    }
    public SelectionType GetSelectionType()
    {
        return selectionType;
    }

}//EndClasss
