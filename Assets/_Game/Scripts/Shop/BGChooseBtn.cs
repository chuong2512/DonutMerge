using UnityEngine;

namespace ChuongCustom
{
    public class BGChooseBtn : ShopChooseButton
    {
        protected override void SetImage()
        {
            image.sprite = _gameData.ShopSo.GetBGData(ID).image;
        }
        
        protected override void UnlockItem()
        {
            _gameData.playerData.AddBG(ID);
        }

        protected override void ChooseItem()
        {
            _gameData.playerData.ChooseBG(ID);
        }

        public override bool IsUnlock()
        {
            return _gameData.playerData.CheckContainBG(ID);
        }
        
        public override bool IsChoosing()
        {
            return _gameData.playerData.choosingBG == ID;
        }

        protected override int GetPrice()
        {
            return _gameData.ShopSo.GetBGData(ID).price;
        }
    }
}