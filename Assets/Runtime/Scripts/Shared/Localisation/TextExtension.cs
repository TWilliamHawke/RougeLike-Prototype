using UnityEngine.UI;
using TMPro;

public static class TextExtension
{

	public static void SetLocalisedText(this Text textComponent, string key)
	{
		textComponent.text = LocalDictionary.GetLocalisedString(key);
	}

	public static void SetLocalisedText(this TextMeshProUGUI textComponent, string key)
	{
		textComponent.text = LocalDictionary.GetLocalisedString(key);
	}


}


