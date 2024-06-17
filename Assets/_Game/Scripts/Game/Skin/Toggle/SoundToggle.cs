namespace ChuongCustom
{
    public class SoundToggle : SettingToggle
    {
        private void OnEnable()
        {
            Toggle(AudioManager.Instance.CheckSound);
        }

        protected override void OnClickToggle()
        {
            AudioManager.Instance.ToggleMusic();
            Toggle(AudioManager.Instance.CheckSound);
        }
    }
}