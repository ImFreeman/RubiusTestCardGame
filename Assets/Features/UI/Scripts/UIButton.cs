using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Features.UI.Scripts
{
    public class UIButton : MonoBehaviour
    {
        public event Action OnClickEvent;

        [SerializeField] private Button button;

        public void SetActive(bool value)
        {
            button.interactable = value;
        }

        private void Start()
        {
            button.onClick.AddListener(OnClickHandler);
        }

        private void OnClickHandler()
        {
            OnClickEvent?.Invoke();
        }

    }
}