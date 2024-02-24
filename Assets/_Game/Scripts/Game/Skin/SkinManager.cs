using ChuongCustom;
using UnityEngine;

namespace _Game.Scripts.Game
{
    public class SkinManager : Singleton<SkinManager>
    {
        private const string LoadPath = "Skin/Skin ";
        private SkinSO _skinsData;
        
        protected override void Awake()
        {
            base.Awake();
            LoadSkinData(GameDataManager.Instance.playerData.choosingSkin);
        }

        private void LoadSkinData(int id)
        {
            _skinsData = Resources.Load<SkinSO>($"{LoadPath}{id}");
        }
        
        public static SkinSO SkinData(int id)
        {
            return Resources.Load<SkinSO>($"{LoadPath}{id}");
        }

        public Sprite GetSkin(int level)
        {
            return _skinsData[level];
        }
    }
}