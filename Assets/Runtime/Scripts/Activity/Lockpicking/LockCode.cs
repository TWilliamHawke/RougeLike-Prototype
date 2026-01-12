using System.Collections.Generic;
using UnityEngine;

namespace Lockpicking
{
    public class LockCode
    {
        public int lockLevel { get; init; }
        public int pegCount => lockLevel + 3;

        List<int> _code;
        public string codeString => string.Join(",", _code);

        public LockCode(int lockLevel)
        {
           this.lockLevel = lockLevel;
            _code = new(pegCount);

            int firstPegIdx = GetFirstPegIndex(pegCount);
            _code.Add(firstPegIdx);

            int pegsBeforeFirst = firstPegIdx;
            int pegsAfterFirst = pegCount - 1 - firstPegIdx;
            SelectNextPeg(pegsBeforeFirst, pegsAfterFirst);
            Debug.Log(string.Join(",", _code));
        }

        public bool Validate(List<int> order)
        {
            if (order.Count > _code.Count) return false;
            if (order.Count < 2) return true;
            int firstIndex = _code.IndexOf(order[0]);
            if (firstIndex < 0) return false;

            for (int i = 1; i < order.Count; i++)
            {
                int targetIdx = firstIndex + i;
                if (targetIdx >= _code.Count) return false;

                if (_code[targetIdx] != order[i]) return false;
                if (_code.IndexOf(order[i]) < _code.IndexOf(order[i - 1]))
                {
                    return false;
                }
            }

            return true;
        }

        private int GetFirstPegIndex(int pegCount)
        {
            //emulation of normal distribution
            float rawPegIdx = (Random.Range(0, pegCount) + Random.Range(0, pegCount)) * .5f;
            int firstPegIdx = Mathf.FloorToInt(rawPegIdx);
            if(rawPegIdx - firstPegIdx != 0)
            {
                firstPegIdx = firstPegIdx + Random.Range(0, 2);
            }

            return firstPegIdx;
        }

        private void SelectNextPeg(int pegsBeforeFirst, int pegsAfterFirst)
        {
            if (pegsBeforeFirst == 0 && pegsAfterFirst == 0) return;
            int totalPegs = pegsBeforeFirst + pegsAfterFirst;
            int selectedPeg = Random.Range(0, totalPegs);

            if (selectedPeg < pegsBeforeFirst)
            {
                _code.Add(pegsBeforeFirst - 1);
                pegsBeforeFirst--;
            }
            else
            {
                _code.Add(pegCount - pegsAfterFirst);
                pegsAfterFirst--;
            }

            SelectNextPeg(pegsBeforeFirst, pegsAfterFirst);
        }
    }
}
