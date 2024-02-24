using System;
using System.Collections;
using System.Collections.Generic;
using ChuongCustom;
using UnityEngine;

public class BGInGame : MonoBehaviour
{
    public SpriteRenderer bg;

    private GameDataManager _gameData;

    private void Start()
    {
        _gameData = GameDataManager.Instance;

        bg.sprite = _gameData.ShopSo.GetBGData(_gameData.playerData.choosingBG).bg;
    }
}