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
    [SerializeField] private ShopPanel _shopPanel;

    private GameDataManager _dataManager => GameDataManager.Instance;

    private void Start()
    {
        _highScore.text =   PlayerPrefs.GetInt("highScore").ToString();

        _startBtn.onClick.AddListener(OnClickButton);
        _homeBtn.onClick.AddListener(OnClickButtonHome);
        _shopBtn.onClick.AddListener(OnClickButtonShop);
    }

    private void OnClickButtonShop()
    {
        //ToastManager.Instance.ShowMessageToast("Tính năng sẽ sớm đc cập nhật");
        //ScreenManager.Instance.OpenScreen(ScreenType.SHOP);
        _shopPanel.Open();
    }

    private void OnClickButtonHome()
    {
        ScreenManager.Instance.OpenScreen(ScreenType.HomeScreen);
        _shopPanel.Close();
    }

    private void OnClickButton()
    {
        SceneLoader.Instance.Play();
    }
}