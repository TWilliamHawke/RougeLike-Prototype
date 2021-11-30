using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionsExtension
{
	public static T GetRandom<T>(this T[] array)
	{
		int randomIndex = Random.Range(0, array.Length);
		return array[randomIndex];
	}

	public static T GetRandom<T>(this List<T> array)
	{
		int randomIndex = Random.Range(0, array.Count);
		return array[randomIndex];
	}
}
