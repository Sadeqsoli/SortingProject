using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionInputToInput : MonoBehaviour, ISelection
{
    [SerializeField] TMP_InputField countInput;
    public SelectionType selectionType = SelectionType.InputToInput;
    [SerializeField] TMP_InputField minInput;
    [SerializeField] TMP_InputField maxInput;
    [SerializeField] TextMeshProUGUI WarningTXT;
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
        if (string.IsNullOrEmpty(minInput.text))
        {
            LogWarnings("Input for min number is Empty!", _Color.R_DRed);
            minInput.targetGraphic.color = _Color.R_SLRed;
            return false;
        }
        if (string.IsNullOrEmpty(maxInput.text))
        {
            LogWarnings("Input for max number is Empty!", _Color.R_DRed);
            maxInput.targetGraphic.color = _Color.R_SLRed;
            return false;
        }
        short maxNumb = -1;
        if (!short.TryParse(this.maxInput.text, out maxNumb))
        {
            LogWarnings("Couldn't parse Min Input Number, Try again!", _Color.R_DRed);

            maxInput.targetGraphic.color = _Color.R_SLRed;
            return false;
        }
        short minNumb = -1;
        if (!short.TryParse(this.minInput.text, out minNumb))
        {
            LogWarnings("Couldn't parse Max Input Number, Try again!", _Color.R_DRed);
            minInput.targetGraphic.color = _Color.R_SLRed;
            return false;
        }

        var difference = maxNumb - minNumb;
        if (countNumber > difference)
        {
            LogWarnings("Count number is bigger than the chosen domain!", _Color.R_DRed);
            countInput.targetGraphic.color = _Color.R_SLRed;
            return false;
        }
        var inputToInput = new NumbSelection(countNumber, minNumb, maxNumb);
        if (inputToInput != null)
        {
            EventManager.NumberSelectionEvent?.Invoke(inputToInput);
        }
        else
        {
            LogWarnings("Number Selction is Null!", _Color.R_DRed);
            return false;
        }
        Debug.Log("difference: " + difference);
        Debug.Log("Count: " + inputToInput.Count);
        Debug.Log("Min: " + inputToInput.Min);
        Debug.Log("Max: " + inputToInput.Max);
        maxInput.targetGraphic.color = _Color.G_SSLGreen;
        minInput.targetGraphic.color = _Color.G_SSLGreen;
        countInput.targetGraphic.color = _Color.G_SSLGreen;
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
        minInput.targetGraphic.color = _Color.BaseWhite;
        countInput.targetGraphic.color = _Color.BaseWhite;
    }

    public SelectionType GetSelectionType()
    {
        return selectionType;
    }

}//EndClasss
