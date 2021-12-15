using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EditorHelpers
{
	public static float lineHeight => EditorGUIUtility.singleLineHeight + 2;

	public static Rect RectToSingleLine(Rect position)
	{
		return new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
	}
}
