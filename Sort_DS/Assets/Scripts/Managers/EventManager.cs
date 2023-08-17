
using System.Collections.Generic;
using UnityEngine.Events;

public static class EventManager 
{
    public static UE_Numb NumberSelectionEvent = new UE_Numb();
    public static UnityEvent<ISelection> SelectionSenderEvent = new UnityEvent<ISelection>();
    public static UnityEvent<List<int>> ListSenderEvent = new UnityEvent<List<int>>();
}
