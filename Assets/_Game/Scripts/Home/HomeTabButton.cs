using UnityEngine;
using UnityEngine.UI;

namespace ChuongCustom
{
    public class HomeTabButton : AButton
    {
        public Image image;
        public Color onChooseImg, unChooseImg;

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
            image.color = onChooseImg;
        }

        public override void OnUnChoose()
        {
            image.color = unChooseImg;
        }
    }
}