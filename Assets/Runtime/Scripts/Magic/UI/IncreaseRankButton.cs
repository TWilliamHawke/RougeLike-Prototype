using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Magic.UI
{
    [RequireComponent(typeof(Button))]
    public class IncreaseRankButton : MonoBehaviour
    {
        [SerializeField] Spellbook _spellbook;

        KnownSpellData _spellData;

        public void Init()
        {
            KnownSpellData.OnChangeData += UpdateState;
        }

        void OnDestroy()
        {
            KnownSpellData.OnChangeData -= UpdateState;
        }



        public void UpdateState(KnownSpellData data)
        {
            _spellData = data;
            CheckButtonState();
        }

        //used in button click handler
        public void IncreaseSpellRank()
        {
            if (_spellData == null) return;
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
            //TODO check active conditions

        }


    }
}