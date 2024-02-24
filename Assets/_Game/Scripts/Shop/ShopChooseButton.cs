﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChuongCustom
{
    public abstract class ShopChooseButton : AButton
    {
        public int ID;

        [Header("Config")] 
        public Image image;
        public GameObject onChooose, lockObj;
        public TextMeshProUGUI price;

        protected GameDataManager _gameData;

        private void Start()
        {
            _gameData = GameDataManager.Instance;

            _button.onClick.AddListener(OnClickButton);
        }

        private void SetPrice()
        {
            price.SetText($"{GetPrice()} <sprite=0>");
        }

        private void OnClickButton()
        {
            if (IsUnlock()) return;
            if (_gameData.playerData.Coin >= GetPrice())
            {
                _gameData.playerData.Coin -= GetPrice();

                UnlockItem();
            }
            else
            {
                ToastManager.Instance.ShowMessageToast("Tính năng sẽ sớm đc cập nhật");
            }
        }

        protected abstract void SetImage();
        protected abstract void UnlockItem();
        protected abstract void ChooseItem();

        public abstract bool IsUnlock();
        public abstract bool IsChoosing();

        protected abstract int GetPrice();

        public override void Choose(bool b)
        {
            if (b)
            {
                OnChoose();
            }
            else
            {
                OnUnChoose();
            }
        }

        public override void OnChoose()
        {
            onChooose.SetActive(true);
            ChooseItem();
        }

        public override void OnUnChoose()
        {
            onChooose.SetActive(false);
        }

        public void Init(int ID)
        {
            this.ID = ID;

            SetImage();
            SetPrice();

            lockObj.SetActive(IsUnlock());
            
            onChooose.SetActive(IsChoosing());
        }
    }
}