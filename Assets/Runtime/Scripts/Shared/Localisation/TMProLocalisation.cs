using TMPro;
using UnityEngine;

namespace Localisation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TMProLocalisation : MonoBehaviour
    {
        [LocalisationKey]
        [SerializeField] string _localisationKey;

        private void Start()
        {
			var textComp = GetComponent<TextMeshProUGUI>();
			textComp.SetLocalisedText(_localisationKey);
        }

    }
}


