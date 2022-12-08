using UnityEngine;
using UnityEngine.UI;

namespace Localisation
{
    [RequireComponent(typeof(Text))]
	public class TextLocalisation : MonoBehaviour
	{
        [SerializeField] string _localisationKey;

        private void Start()
        {
			var textComp = GetComponent<Text>();
			textComp.SelLocalisedText(_localisationKey);
        }
	}
}


