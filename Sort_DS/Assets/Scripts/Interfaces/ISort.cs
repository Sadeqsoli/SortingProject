
using System.Collections.Generic;

public interface ISort 
{
    int[] Sort(int[] thaList);
    void Deselect();
    SortType GetSortType();
}
