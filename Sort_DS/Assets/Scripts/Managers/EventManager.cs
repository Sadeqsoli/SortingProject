
using System.Collections.Generic;
using UnityEngine.Events;

public class EventManager 
{
    public static UE_Numb NumberSelectionEvent = new UE_Numb();
    public static UnityEvent<ISelection> SelectionSenderEvent = new UnityEvent<ISelection>();
    public static UnityEvent<int[]> ListSenderEvent = new UnityEvent<int[]>();
    public static UnityEvent<ISort> SortSenderEvent = new UnityEvent<ISort>();
}
