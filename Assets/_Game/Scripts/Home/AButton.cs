using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ChuongCustom
{
    public abstract class AButton : MonoBehaviour
    {
        [SerializeField] protected Button _button;

        private void OnValidate()
        {
            _button = GetComponent<Button>();
        }

        public void AddListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void RemoveListener()
        {
            _button.onClick.RemoveAllListeners();
        }

        public abstract void Choose(bool b);
        public abstract void OnChoose();
        public abstract void OnUnChoose();
    }
}