using UnityEngine;

namespace ChuongCustom
{
    public class VFXToggle : SettingToggle
    {
        private void OnEnable()
        {
            Toggle(AudioManager.Instance.CheckSFX);
        }

        protected override void OnClickToggle()
        {
            AudioManager.Instance.ToggleSfx();
            Toggle(AudioManager.Instance.CheckSFX);
        }
    }
}