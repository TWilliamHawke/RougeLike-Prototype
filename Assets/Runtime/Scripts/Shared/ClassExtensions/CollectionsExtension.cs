using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Rng = System.Random;

public static class CollectionsExtension
{
    public static T GetRandom<T>(this T[] array)
    {
        int randomIndex = Random.Range(0, array.Length);
        return array[randomIndex];
    }

    public static T GetRandom<T>(this T[] array, Rng rng)
    {
        int randomIndex = rng.Next(array.Length);
        return array[randomIndex];
    }

    public static T GetRandom<T>(this List<T> list)
    {
        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }

    public static T GetRandom<T>(this List<T> list, Rng rng)
    {
        int randomIndex = rng.Next(list.Count);
        return list[randomIndex];
    }

    public static void ForEach<T>(this IEnumerable<T> source, System.Action<T> action)
    {
        foreach (T item in source)
        {
            action(item);
        }
    }

    public static T GetRandonByWeight<T>(this IEnumerable<T> list, System.Func<T, int> selector)
    {
        var result = list.FirstOrDefault();
        int totalWeight = 0;

        foreach(var element in list)
        {
            totalWeight += selector(element);
        }

        int randomWeight = Random.Range(0, totalWeight);
        totalWeight = 0;

        foreach(var element in list)
        {
            result = element;
            totalWeight += selector(element);

            if (totalWeight >= randomWeight) break;
        }

        return result;
    }
}
