using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Utils;
using ChuongCustom;
using TMPro;
using UnityEngine;

public class CoinTMP : MonoBehaviour
{
    public TextMeshProUGUI coinTMP;

    private GameDataManager _gameData;

    void Start()
    {
        _gameData = GameDataManager.Instance;

        ShowCoin();

        GameAction.OnChangeCoin += ShowCoin;
    }

    private void OnDestroy()
    {
        GameAction.OnChangeCoin -= ShowCoin;
    }

    private void ShowCoin(int coin)
    {
        ShowCoin();
    }

    private void ShowCoin()
    {
        coinTMP.SetText(_gameData.playerData.Coin.ConvertCurrency());
    }
}