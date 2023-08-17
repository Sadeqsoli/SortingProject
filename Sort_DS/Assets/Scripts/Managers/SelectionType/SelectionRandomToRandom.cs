using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionRandomToRandom : MonoBehaviour, ISelection
{
    public SelectionType selectionType = SelectionType.RandomToRandom;
    [SerializeField] TextMeshProUGUI MinRandomTXT;
    [SerializeField] TextMeshProUGUI MaxRandomTXT;
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
        var minNumb = (short)Random.Range(0, 5000);
        var maxNumb = (short)Random.Range(minNumb + _mCountSelection, 9999);

        var randomTorandom = new NumbSelection(_mCountSelection, minNumb, maxNumb);
        if (randomTorandom != null)
        {
            EventManager.NumberSelectionEvent?.Invoke(randomTorandom);
        }
        else
        {
            Debug.Log("NumbSelction is Null!");
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
    }

    public SelectionType GetSelectionType()
    {
        return selectionType;
    }
}//EndClasss
