using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionRandomToRandom : MonoBehaviour, ISelection
{
    [SerializeField] TMP_InputField countInput;
    public SelectionType selectionType = SelectionType.RandomToRandom;
    [SerializeField] TextMeshProUGUI MinRandomTXT;
    [SerializeField] TextMeshProUGUI MaxRandomTXT;
    [SerializeField] TextMeshProUGUI WarningTXT;
    short minNumb = 0;
    short maxNumb = 9999;
    Button ChoosingSelctionBTN;
    Image ChoosingSelctionIMG;

    const int Half = 5000;
    const int Full = 9999;

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

        var minNumb = (short)Random.Range(0, Half);

        var maxFloorNumb = minNumb + countNumber;
        if (maxFloorNumb + countNumber >= Full)
            maxFloorNumb -= (countNumber /2);

        var maxNumb = (short)Random.Range(maxFloorNumb, Full);

        var difference = maxNumb - minNumb;
        if (countNumber > difference)
        {
            LogWarnings("Count number is bigger than the chosen domain!", _Color.R_DRed);
            countInput.targetGraphic.color = _Color.R_SLRed;
            return false;
        }

        var randomTorandom = new NumbSelection(countNumber, minNumb, maxNumb);
        if (randomTorandom != null)
        {
            EventManager.NumberSelectionEvent?.Invoke(randomTorandom);
        }
        else
        {
            LogWarnings("Number Selction is Null!", _Color.R_DRed);
            return false;
        }
        MinRandomTXT.text = minNumb.ToString();
        MinRandomTXT.color = _Color.R_DRed;
        MaxRandomTXT.text = maxNumb.ToString();
        MaxRandomTXT.color = _Color.G_DGreen;
        Debug.Log("Count: " + randomTorandom.Count);
        Debug.Log("Min: " + randomTorandom.Min);
        Debug.Log("Max: " + randomTorandom.Max);
        ChoosingSelctionIMG.color = _Color.Y_LOlive;
        countInput.targetGraphic.color = _Color.G_SLGreen;
        LogWarnings("We have the right selection!", Color.black);
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
    }

    public SelectionType GetSelectionType()
    {
        return selectionType;
    }
}//EndClasss
