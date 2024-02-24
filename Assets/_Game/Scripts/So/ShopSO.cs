using System;
using System.Linq;
using UnityEngine;

namespace ChuongCustom
{
    [CreateAssetMenu(fileName = "Data Shop", menuName = "ScriptableObjects/Shop SO", order = 1)]
    public class ShopSO : ScriptableObject
    {
        public BgGameData[] BGs;
        public SkinData[] Skins;
        public SkillData[] Skills;

        public BgGameData GetBGData(int ID)
        {
            return BGs.ToList().Find(data => data.ID == ID);
        }

        public ShopData GetSkinData(int ID)
        {
            return Skins.ToList().Find(data => data.ID == ID);
        }

        public ShopData GetSkillData(int ID)
        {
            return Skills.ToList().Find(data => data.ID == ID);
        }
    }

    [Serializable]
    public class BgGameData : ShopData
    {
        public Sprite bg;
    }

    [Serializable]
    public class SkinData : ShopData
    {
    }

    [Serializable]
    public class SkillData : ShopData
    {
    }

    [Serializable]
    public class ShopData
    {
        public int ID;
        public Sprite image;
        public int price;
    }
}