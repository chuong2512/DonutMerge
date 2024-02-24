using System;
using UnityEngine;

namespace _Game.Scripts.Game
{
    public class FruitSkinInGame : FruitSkin
    {
        [SerializeField] private int level;
        
        private void Start()
        {
            SetSkin(level);
        }
        
        private void SetSkin(int lv)
        {
            SetSkin(SkinManager.Instance.GetSkin(lv));
        }
    }
}