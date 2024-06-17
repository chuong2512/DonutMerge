using ChuongCustom;

namespace GameCore.Runtime {
    using MoreMountains.NiceVibrations;
    using UnityEngine;

    public class NiceVibration : PersistentSingleton<NiceVibration>{
        private bool enable;

        public bool Enable {
            get => PlayerPrefs.GetFloat("GameVibration") > 0.1f;
            set
            {
                enable = value;
                PlayerPrefs.SetFloat("GameVibration", value ? 1f : 0.0f);
            }
        }

        protected override void Awake() {
            base.Awake();
            enable = Enable;
            MMVibrationManager.iOSInitializeHaptics ();
        }
        
        protected virtual void OnDisable()
        {
            MMVibrationManager.iOSReleaseHaptics ();
        }
        

        public void Light() {
            if(!enable) return;
            MMVibrationManager.Haptic (HapticTypes.LightImpact);
        }

        public void Medium() {
            if(!enable) return;
            MMVibrationManager.Haptic (HapticTypes.MediumImpact);      
        }

        public void Heavy() {
            if(!enable) return;
            MMVibrationManager.Haptic (HapticTypes.HeavyImpact);
        }

        public void Selection() {
            if(!enable) return;
            MMVibrationManager.Haptic (HapticTypes.Selection);
        }

        public void Success() {
            if(!enable) return;
            MMVibrationManager.Haptic (HapticTypes.Success);
        }

        public void Warning() {
            if(!enable) return;
            MMVibrationManager.Haptic (HapticTypes.Warning);
        }

        public void Failure() {
            if(!enable) return;
            MMVibrationManager.Haptic (HapticTypes.Failure);
        }
    }
}