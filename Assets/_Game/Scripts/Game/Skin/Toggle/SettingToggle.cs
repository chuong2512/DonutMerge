using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ChuongCustom
{
    [RequireComponent(typeof(Button))]
    public abstract class SettingToggle : MonoBehaviour
    {
        public Button button;

        public Image imgButton;
        public Sprite onSpr, offSpr;

        private void OnValidate()
        {
            button = GetComponent<Button>();
            imgButton = GetComponent<Image>();
        }

        private void Start()
        {
            button.onClick.AddListener(OnClickToggle);
        }

        protected abstract void OnClickToggle();

        protected void Toggle(bool b)
        {
            imgButton.sprite = b ? onSpr : offSpr;
        }
    }
}