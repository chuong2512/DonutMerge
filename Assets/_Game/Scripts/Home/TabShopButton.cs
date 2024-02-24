using UnityEngine;
using UnityEngine.UI;

namespace ChuongCustom
{
    public class TabShopButton : AButton
    {
        public Image image;
        public Sprite onChooseImg, unChooseImg;

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
            image.sprite = onChooseImg;
        }

        public override void OnUnChoose()
        {
            image.sprite = unChooseImg;
        }
    }
}