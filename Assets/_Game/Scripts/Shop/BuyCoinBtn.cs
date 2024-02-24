using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChuongCustom
{
    public class BuyCoinBtn : MonoBehaviour
    {
        [SerializeField] private string package_id = "mua_vang_goi_1";

        [SerializeField] private int _amount;
        [SerializeField] private float _price;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _amountText, _priceText;

        private PlayerData _player;

        private void Start()
        {
            _player = GameDataManager.Instance.playerData;

            _button.onClick.AddListener(OnClickButton);

            _amountText.text = $"x{_amount}";
            /*_priceText.text = $"$ {_price}";*/
        }

        private void OnClickButton()
        {
            IAPManager.OnPurchaseSuccess = AddGem;
            IAPManager.Instance.BuyProductID(package_id);
        }

        private void AddGem()
        {
            _player.Coin += _amount;
        }
    }
}