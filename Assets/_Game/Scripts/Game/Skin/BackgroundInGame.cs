using System;
using ChuongCustom;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Game
{
    [RequireComponent(typeof(Image))]
    public class BackgroundInGame : MonoBehaviour
    {
        [SerializeField] private Sprite[] bgSprites;
        private Image _bgImage;

        private void Awake()
        {
            _bgImage = GetComponent<Image>();
            ChangeBg(GameDataManager.Instance.playerData.choosingBG);
        }

        private void ChangeBg(int id)
        {
            _bgImage.sprite = bgSprites[id];
        }
    }
}