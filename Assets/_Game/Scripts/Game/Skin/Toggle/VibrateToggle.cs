using GameCore.Runtime;

namespace ChuongCustom
{
    public class VibrateToggle : SettingToggle
    {
        private void OnEnable()
        {
            Toggle(NiceVibration.Instance.Enable);
        }

        protected override void OnClickToggle()
        {
            NiceVibration.Instance.Enable = !NiceVibration.Instance.Enable;
            Toggle(NiceVibration.Instance.Enable);
        }
    }
}