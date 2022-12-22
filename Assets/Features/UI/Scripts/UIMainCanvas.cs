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
        private void Start()
        {
            playButton.OnClickEvent += () => 
            { 
                PlayButtonClickEvent?.Invoke();
            };
            cancelButton.OnClickEvent += () => 
            {
                CancelButtonClickEvent?.Invoke(); 
            };
            dropdown.ChangeValueEvent += (sender, value) => 
            {
                DropDownValueChange?.Invoke(this, value);
            };
        }
    }
}