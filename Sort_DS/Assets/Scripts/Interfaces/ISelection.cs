using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelection 
{
    public SelectionType GetSelectionType();
    public void ResetSelection();
    public bool SelectingNumbers();
}
