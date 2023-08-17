using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Numb : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberTXT;
    [SerializeField] Image numberIMG;


    public int Number { get; private set; }


    public void InitNumber(int numb)
    {
        Number = numb;
        numberTXT.text = numb.ToString();
        numberTXT.color = Color.black;
        numberIMG.color = _Color.Y_LOlive;
    }
}