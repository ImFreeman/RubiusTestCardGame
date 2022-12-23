using System;
using UnityEngine;

namespace Assets.Features.UI.Scripts
{
    public class UIMainCanvas : MonoBehaviour
    {
        public event Action PlayButtonClickEvent;
        public event Action CancelButtonClickEvent;
        public event EventHandler<int> DropDownValueChange;

        public Transform CardContainer => cardContainer;

        [SerializeField] private UIDropdown dropdown;
        [SerializeField] private UIButton playButton;
        [SerializeField] private UIButton cancelButton;
        [SerializeField] private Transform cardContainer;

        public void SetPlayButtonActive(bool value)
        {
            playButton.SetActive(value);
        }

        public void SetCancelButtonActive(bool value)
        {
            cancelButton.SetActive(value);
        }

        private void Start()
        {
            playButton.OnClickEvent += PlayButtonClickHandler;
            cancelButton.OnClickEvent += CancelButtonClickHandler;
            dropdown.ChangeValueEvent += DropdownValueChangeHandler;
        }

        private void PlayButtonClickHandler()
        {
            PlayButtonClickEvent?.Invoke();
        }

        private void CancelButtonClickHandler()
        {
            CancelButtonClickEvent?.Invoke();
        }

        private void DropdownValueChangeHandler(object sender, int value)
        {
            DropDownValueChange?.Invoke(this, value);
        }

        private void OnDestroy()
        {
            playButton.OnClickEvent -= PlayButtonClickHandler;
            cancelButton.OnClickEvent -= CancelButtonClickHandler;
            dropdown.ChangeValueEvent -= DropdownValueChangeHandler;
        }
    }
}