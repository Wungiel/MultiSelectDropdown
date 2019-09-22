using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensions
{
    public static T ContainsOptimized<T>(this IEnumerable<T> list, int id) where T: IIDEquatable
    {
        foreach(T objectToCheck in list)
        {
            if (objectToCheck.CompareById(id) == true)
            {
                return objectToCheck;
            }
        }

        return default;
    }
}
