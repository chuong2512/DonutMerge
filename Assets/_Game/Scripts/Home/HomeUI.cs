using System;
using System.Collections;
using System.Collections.Generic;
using ChuongCustom;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _homeBtn, _shopBtn;

    [SerializeField] private TextMeshProUGUI _highScore;

    private GameDataManager _dataManager => GameDataManager.Instance;

    private void Start()
    {
        _highScore.text = _dataManager.playerData.GetMapWithID(0).highScore.ToString();

        _startBtn.onClick.AddListener(OnClickButton);
        _homeBtn.onClick.AddListener(OnClickButtonHome);
        _shopBtn.onClick.AddListener(OnClickButtonShop);
    }

    private void OnClickButtonShop()
    {
        ToastManager.Instance.ShowMessageToast("Tính năng sẽ sớm đc cập nhật");
        //ScreenManager.Instance.OpenScreen(ScreenType.SHOP);
    }

    private void OnClickButtonHome()
    {
        ScreenManager.Instance.OpenScreen(ScreenType.HomeScreen);
    }

    private void OnClickButton()
    {
        SceneLoader.Instance.Play();
    }
}