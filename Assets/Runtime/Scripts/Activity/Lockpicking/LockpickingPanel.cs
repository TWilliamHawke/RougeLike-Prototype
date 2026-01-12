using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lockpicking
{
    public class LockpickingPanel : MonoBehaviour
    {
        [SerializeField] Pick _pick;
        [SerializeField] CustomSlider _slider;
        [SerializeField] LockSegmentsWrapper _lockSegments;
        [Header("Sounds")]
        [SerializeField] AudioClip _moveUpSound;
        [SerializeField] AudioClip _moveDownSound;
        [SerializeField] AudioClip _unlockSound;

        [InjectField] IAudioController _audioController;

        public event UnityAction OnUnlock;

        LockCode _lockCode;
        List<int> _selectedPegs = new();

        void Start()
        {
            _pick.SyncWithSlider(_slider);
            _lockSegments.OnPegTouch += ValidateCode;
            _slider.ResetValue();
            //OpenScreen(50);
        }

        public void OpenScreen(int lockLevel)
        {
            _lockCode = new LockCode(lockLevel);
            _slider.ResetValue();
            gameObject.SetActive(true);
            _selectedPegs.Clear();
            _lockSegments.UpdateSegments(_lockCode.pegCount);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ValidateCode(int segmentIdx)
        {
            if (_selectedPegs.Contains(segmentIdx)) return;
            _selectedPegs.Add(segmentIdx); //needs for validation
            _lockSegments.MovePegUp(segmentIdx);

            if (_lockCode.Validate(_selectedPegs))
            {
                if (_selectedPegs.Count == _lockCode.pegCount)
                {
                    _audioController.PlaySound(_unlockSound);
                    StartCoroutine(CloseScreen());
                }
                else
                {
                    _audioController.PlaySound(_moveUpSound);
                }
            }
            else
            {
                _lockSegments.MovePegsDown(segmentIdx);
                _selectedPegs.Clear();
                _selectedPegs.Add(segmentIdx);
                _audioController.PlaySound(_moveDownSound);
            }
        }

        private IEnumerator CloseScreen()
        {
            yield return new WaitForSeconds(1f);
            _selectedPegs.Clear();
            OnUnlock?.Invoke();
        }
    }
}
