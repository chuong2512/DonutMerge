using System;
using UnityEngine;

namespace _Game.Scripts.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FruitSkin : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected void SetSkin(int skinID)
        {
            
        }
        
        protected void SetSkin(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}