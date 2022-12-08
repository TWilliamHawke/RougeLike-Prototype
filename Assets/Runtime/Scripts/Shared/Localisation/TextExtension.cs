using UnityEngine.UI;
using TMPro;

public static class TextExtension
{

	public static void SelLocalisedText(this Text textComponent, string key)
	{
		textComponent.text = LocalDictionary.GetLocalisedString(key);
	}

	public static void SelLocalisedText(this TextMeshProUGUI textComponent, string key)
	{
		textComponent.text = LocalDictionary.GetLocalisedString(key);
	}


}


