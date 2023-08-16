using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MathGen
{
    private static System.Random random = new System.Random();

    public static List<int> GenerateRandomWithoutRepeat(int count, int min, int max)
    {
        bool isEqualWithCurrentList = true;
        List<int> result = new List<int>();
        if (count > 1)
        {
            while (isEqualWithCurrentList)
            {
                result = GenerateRandom(count, min, max);
                string randomListOfInt = string.Join(",", result);
                if (!IsEqual(result, SetListInOrder(count)))
                {
                    isEqualWithCurrentList = false;
                    Debug.Log(randomListOfInt);
                }
            }
        }
        else
        {
            result.Add(min);
        }
        return result;
    }
    public static List<int> GenerateRandom(int count, int min, int max)
    {
        if (max <= min || count < 0 ||
                (count > max - min && max - min > 0))
        {
            throw new ArgumentOutOfRangeException("Range " + min + " to " + max +
                    " (" + ((Int64)max - (Int64)min) + " values), or count " + count + " is illegal");
        }

        HashSet<int> candidates = new HashSet<int>();

        for (int top = max - count; top < max; top++)
        {
            if (!candidates.Add(random.Next(min, top + 1)))
            {
                candidates.Add(top);
            }
        }

        List<int> result = candidates.ToList();

        for (int i = result.Count - 1; i > 0; i--)
        {
            int k = random.Next(i + 1);
            int tmp = result[k];
            result[k] = result[i];
            result[i] = tmp;
        }
        return result;
    }

    static List<int> SetListInOrder(int count)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < count; i++)
        {
            list.Add(i);
        }
        return list;
    }

    static bool IsEqual(List<int> generatedList, List<int> correctList)
    {
        var j = 0;
        var k = 0;
        bool isEqual = true;
        int[] zeroOneList = new int[correctList.Count];
        for (var i = 0; i < correctList.Count; i++)
        {
            j = correctList[i];
            k = generatedList[i];
            if (j == k) { zeroOneList[i] = 1; }

        }
        for (var x = 0; x < correctList.Count; x++)
        {
            if (zeroOneList[x] != 1) { isEqual = false; break; }
        }

        return isEqual;
    }

}//EndClasssss
