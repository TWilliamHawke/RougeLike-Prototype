using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
