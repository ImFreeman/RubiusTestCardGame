using System;
using TMPro;
using UnityEngine;

namespace Assets.Features.UI.Scripts
{
    public class UIDropdown : MonoBehaviour
    {
        public event EventHandler<int> ChangeValueEvent;
        [SerializeField] private TMP_Dropdown dropdown;

        private void Start()
        {
            dropdown.onValueChanged.AddListener(ChangeValueHandler);
        }

        private void ChangeValueHandler(int value)
        {
            ChangeValueEvent?.Invoke(this, value);
        }
    }
}