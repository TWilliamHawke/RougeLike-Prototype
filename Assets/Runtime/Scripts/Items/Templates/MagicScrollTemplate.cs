using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class MagicScrollTemplate : StaticItemTemplate
    {
        public override IItem CreateItem(int rarity = 0)
        {
            return new MagicScroll(this);
        }

        public override string GetDescription()
        {
            throw new System.NotImplementedException();
        }
    }
}