using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.UI;
using Items;

namespace Magic.UI
{
    [RequireComponent(typeof(Button))]
    public class IncreaseRankButton : MonoBehaviour
    {
        [SerializeField] Spellbook _spellbook;
        [SerializeField] Inventory _inventory;

        KnownSpellData _spellData;

        public void UpdateState(KnownSpellData data)
        {
            _spellData = data;
            CheckButtonState();
        }

        //used in button click handler
        public void IncreaseSpellRank()
        {
            if (_spellData is null) return;
            _spellbook.IncreaseSpellRank(_spellData);
        }

        void CheckButtonState()
        {
            if (_spellData.rank >= _spellbook.maxSpellRank)
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);

            if (_inventory.resources[ResourceType.magicDust] < _spellbook.increaseRankCost)
            {
                GetComponent<Button>().interactable = false;
            }
            else
            {
                GetComponent<Button>().interactable = true;
            }

        }


    }
}