using UnityEngine;

namespace ChuongCustom
{
    public class SkinChooseBtn : ShopChooseButton
    {
        protected override void SetImage()
        {
            image.sprite = _gameData.ShopSo.GetSkinData(ID).image;
        }

        protected override void UnlockItem()
        {
            _gameData.playerData.AddSkin(ID);
        }

        protected override void ChooseItem()
        {
            _gameData.playerData.ChooseSkin(ID);
        }

        public override bool IsUnlock()
        {
            return _gameData.playerData.CheckContainSkin(ID);
        }

        public override bool IsChoosing()
        {
            return _gameData.playerData.choosingSkin == ID;
        }

        protected override int GetPrice()
        {
            return _gameData.ShopSo.GetSkinData(ID).price;
        }
    }
}