using System;
using ChuongCustom;
using UnityEngine;
using UnityEngine.UI;

namespace ChuongCustom
{
    public class BuySkillBtn : MonoBehaviour
    {
        [SerializeField] private int id = 0;
        [SerializeField] private int _amount = 1, _price = 500;
        [SerializeField] private Button _button;
        [SerializeField] private Text _amountText, _priceText;

        private PlayerData _player;

        private void Start()
        {
            _player = GameDataManager.Instance.playerData;

            _button.onClick.AddListener(OnClickButton);

            /*_amountText.text = $"x{_amount}";
            _priceText.text = _price.ToString();*/
        }

        private void OnClickButton()
        {
            if (_player.Coin >= _price)
            {
                _player.Coin -= _price;
                _player.AddNumberSkill(id, _amount);
                ToastManager.Instance.ShowMessageToast("Buy Success!!");
            }
            else
            {
                ToastManager.Instance.ShowMessageToast("Not enough coin!!");
            }
        }
    }
}