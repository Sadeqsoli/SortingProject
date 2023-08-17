using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionInputToInput : MonoBehaviour, ISelection
{
    public SelectionType selectionType = SelectionType.InputToInput;
    [SerializeField] TMP_InputField minInput;
    [SerializeField] TMP_InputField maxInput;
    [SerializeField] TextMeshProUGUI WarningTXT;
    [SerializeField] short _mCountSelection = 100;
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

        if (difference >= 100 || difference <= -100)
        {
            var inputToInput = new NumbSelection(_mCountSelection, minNumb, maxNumb);
            if (inputToInput != null)
            {
                EventManager.NumberSelectionEvent?.Invoke(inputToInput);
            }
            else
            {
                Debug.Log("NumbSelction is Null!");
                return false;
            }
            Debug.Log("difference: " + difference);
            Debug.Log("Count: " + inputToInput.Count);
            Debug.Log("Min: " + inputToInput.Min);
            Debug.Log("Max: " + inputToInput.Max);
            maxInput.targetGraphic.color = _Color.G_SSLGreen;
            minInput.targetGraphic.color = _Color.G_SSLGreen;
            LogWarnings("We have the right selection!", _Color.BlackGray);
            ChoosingSelctionIMG.color = _Color.Y_LOlive;
            return true;
        }
        else
        {
            LogWarnings("At-least 100 between Min and Max numbers", Color.black);
            maxInput.targetGraphic.color = _Color.R_SLRed;
            minInput.targetGraphic.color = _Color.R_SLRed;
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
        maxInput.targetGraphic.color = Color.white;
        minInput.targetGraphic.color = Color.white;
    }

    public SelectionType GetSelectionType()
    {
        return selectionType;
    }

}//EndClasss
