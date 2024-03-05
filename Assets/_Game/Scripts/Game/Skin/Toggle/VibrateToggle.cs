using UnityEngine;

namespace ChuongCustom
{
    public class VibrateToggle : SettingToggle
    {
        private bool check;

        private void OnEnable()
        {
            Toggle(check);
        }

        protected override void OnClickToggle()
        {
            check = !check;
            Toggle(check);
        }
    }
}