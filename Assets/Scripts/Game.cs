using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Utils;
using ChuongCustom;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using JetBrains.Annotations;

public enum SkillType
{
    Destroy = 0,
    X2,
    Roll,
    Hammer
}

public class Game : Singleton<Game>
{
    public List<Fruit> fruitPrefabList;
    public Transform spawnPoint;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI totalCoinText;
    public TextMeshProUGUI highScoreText;
    private int numTotalCoin;
    private int numHighScore;
    public Item HammerItem, X2Item;
    [HideInInspector] public bool isHammer, isX2;
    public int score;

    private Fruit fruit;
    private int fruidId;
    private bool isGameOver;
    private List<Fruit> fruits = new List<Fruit>();

    public TextMeshProUGUI x2Text, HammerText;
    public TextMeshProUGUI rollText, destroyText;
    private int X2Count, HammerCount;
    private int rollCount, destroyCount;
    public GameObject AdHammer, AdX2;

    private Item _itemSpawn;

    [Header("GAME OVER PANEL")] public GameObject GameOverPanel;
    public TextMeshProUGUI GOverBestScore, GOverScore;
    public GameObject Star0, Star1, Star2;

    [Header("SETTINGS PANEL")] public GameObject SettingsPanel;
    public Slider _musicSlider, _sfxSlider;
    private bool isSetting;

    [Header("SHOP PANEL")] public GameObject ShopPanel;
    private bool isShop;

    private float _count;
    private float _deltaSpawnTime = 0.5f;

    private GameDataManager _gameData;

    void Start()
    {
        _gameData = GameDataManager.Instance;

        fruit = SpawnNextFruit();
        CheckHighScore();
        CheckTotalCoin();
        CheckInfo();

        float _sound = PlayerPrefs.GetFloat("SfxVolume");
        float _music = PlayerPrefs.GetFloat("MusicVolume");
        _musicSlider.value = _music;
        _sfxSlider.value = _sound;
    }

    private void CheckInfo()
    {
        X2Count = _gameData.playerData.GetNumberSkill(SkillType.X2);
        x2Text.text = "" + X2Count;

        HammerCount = _gameData.playerData.GetNumberSkill(SkillType.Hammer);
        HammerText.text = "" + HammerCount;

        rollCount = _gameData.playerData.GetNumberSkill(SkillType.Roll);
        rollText.text = "" + rollCount;

        destroyCount = _gameData.playerData.GetNumberSkill(SkillType.Destroy);
        destroyText.text = "" + destroyCount;

        AdHammer.SetActive(false);
        AdX2.SetActive(false);
    }

    private void CheckHighScore()
    {
        numHighScore = PlayerPrefs.GetInt("highScore");
        highScoreText.text = "" + numHighScore;
    }

    private void CheckTotalCoin()
    {
        numTotalCoin = PlayerPrefs.GetInt("totalCoin");
        totalCoinText.text = "" + numTotalCoin;
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        _count -= Time.fixedDeltaTime;

        if (!(_count <= 0))
        {
            return;
        }

        if (!Input.GetMouseButtonUp(0) || isHammer || isX2 || isSetting || isShop) return;
        if (!(Input.mousePosition.y < (Screen.height / 5) * 3)) return;

        _count = _deltaSpawnTime;
        var mousePos = Input.mousePosition;
        var wolrdPos = Camera.main.ScreenToWorldPoint(mousePos);
        var fruitPos = new Vector3(wolrdPos.x, spawnPoint.position.y - 2.2f, 0);

        fruit.transform.position = fruitPos;

        fruit.SetSimulated(true);
        fruit = SpawnNextFruit();
    }

    private Fruit SpawnNextFruit()
    {
        var rand = Random.Range(0, 5);
        var prefab = fruitPrefabList[rand].gameObject;
        var pos = spawnPoint.position;

        return SpawnFruit(prefab, pos);
    }

    private Fruit SpawnFruit(GameObject prefab, Vector3 pos)
    {
        var obj = Instantiate(prefab, pos, Quaternion.identity);
        var f = obj.GetComponent<Fruit>();
        f.SetSimulated(false);
        f.id = fruidId++;

        f.OnLevelUp = (a, b) =>
        {
            if (IsFruitExist(a) && IsFruitExist(b))
            {
                var pos1 = a.gameObject.transform.position;
                var pos2 = b.gameObject.transform.position;
                var pos = (pos1 + pos2) * 0.5f;
                RemoveFruit(a);
                RemoveFruit(b);
                AddScore(a.score);
                var fr = SpawnFruit(a.nextLevelPrefab, pos);
                fr.SetSimulated(true);
                fr.PlayFx();
            }
        };

        f.OnGameOver = () =>
        {
            if (isGameOver == true)
            {
                return;
            }

            OnGameOver();
        };

        fruits.Add(f);
        return f;
    }

    private void OnGameOver()
    {
        isGameOver = true;
        GameOverPanel.SetActive(true);
        CheckStar();

        for (int i = 0; i < fruits.Count; i++)
        {
            fruits[i].SetSimulated(false);
            AddScore(fruits[i].score);
            Destroy(fruits[i].gameObject);
        }

        GOverBestScore.text = "BEST: " + numHighScore;
        GOverScore.text = "" + score;

        PlayerPrefs.SetInt("totalCoin", PlayerPrefs.GetInt("totalCoin") + score);
        CheckTotalCoin();
        if (score > numHighScore)
        {
            numHighScore = score;
            PlayerPrefs.SetInt("highScore", numHighScore);

            GameDataManager.Instance.playerData.SetMaxHighScore(score, 0);
        }

        CheckHighScore();
        fruits.Clear();
    }

    private void CheckStar()
    {
        if ((score - numHighScore) >= 0)
        {
            Star0.SetActive(true);
            Star1.SetActive(true);
            Star2.SetActive(true);
        }
        else
        {
            if ((score / numHighScore) > 0.5f)
            {
                Star0.SetActive(true);
                Star1.SetActive(true);
                Star2.SetActive(false);
            }
            else
            {
                Star0.SetActive(true);
                Star1.SetActive(false);
                Star2.SetActive(false);
            }
        }
    }

    public void Restart()
    {
        AudioManager.Instance.PlaySFX("_button");

        GameOverPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        fruit = SpawnNextFruit();

        score = 0;
        scoreLabel.text = "0";

        isGameOver = false;
        isSetting = false;
        isShop = false;
    }

    public void RestartSetting()
    {
        isGameOver = true;

        for (int i = 0; i < fruits.Count; i++)
        {
            fruits[i].SetSimulated(false);
            AddScore(fruits[i].score);
            Destroy(fruits[i].gameObject);
        }

        fruits.Clear();

        Restart();
    }

    private void RemoveFruit(Fruit f)
    {
        for (int i = 0; i < fruits.Count; i++)
        {
            if (fruits[i].id == f.id)
            {
                fruits.Remove(f);
                Destroy(f.gameObject);
                return;
            }
        }
    }

    private bool IsFruitExist(Fruit f)
    {
        for (int i = 0; i < fruits.Count; i++)
        {
            if (fruits[i].id == f.id)
            {
                return true;
            }
        }

        return false;
    }

    private void AddScore(int score)
    {
        this.score += score;
        scoreLabel.text = $"{this.score}";
    }

    public void ClickRemoveBtn()
    {
        if (isHammer)
        {
            isHammer = false;
            Destroy(_itemSpawn.gameObject);
            return;
        }

        if (HammerCount > 0 && !isHammer && !isX2)
        {
            Item _item = Instantiate(HammerItem);
            var pos = Input.mousePosition;
            _item.transform.position = Camera.main.ScreenToWorldPoint(pos);
            _itemSpawn = _item;
            isHammer = true;
        }

        if (HammerCount == 0 && !isHammer && !isX2)
        {
            //
        }

        AudioManager.Instance.PlaySFX("_button");
    }

    public void CallBackAdsHammer()
    {
        PlayerPrefs.SetInt("numHammer", 5);
        CheckInfo();
    }

    public void ClickX2Btn()
    {
        if (isX2)
        {
            isX2 = false;
            Destroy(_itemSpawn.gameObject);
            return;
        }

        if (X2Count > 0 && !isHammer && !isX2)
        {
            Item _item = Instantiate(X2Item);
            var pos = Input.mousePosition;
            _item.transform.position = Camera.main.ScreenToWorldPoint(pos);
            _itemSpawn = _item;
            isX2 = true;
        }

        if (X2Count == 0 && !isHammer && !isX2)
        {
            // AdmobController.instance.ShowAdReward(AdmobController.TypeReward.X2);
        }

        AudioManager.Instance.PlaySFX("_button");
    }

    public void CallBackAdsX2()
    {
        PlayerPrefs.SetInt("numX2", 5);
        CheckInfo();
    }

    public void RemoveHammer()
    {
        HammerCount -= 1;
        _gameData.playerData.MinusNumberSkill((int) SkillType.Hammer, 1);
        CheckInfo();
    }

    public void RemoveX2()
    {
        X2Count -= 1;
        _gameData.playerData.MinusNumberSkill((int) SkillType.X2, 1);
        CheckInfo();
    }

    public void OpenSettingsPanel()
    {
        isSetting = true;
        SettingsPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("_button");
    }

    public void CloseSettingsPanel()
    {
        AudioManager.Instance.PlaySFX("_button");
        PlayerPrefs.SetFloat("SfxVolume", _sfxSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        isSetting = false;
        SettingsPanel.SetActive(false);
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
        if (_musicSlider.value > 0)
        {
            AudioManager.Instance.musicSource.mute = false;
        }

        if (_musicSlider.value == 0)
        {
            AudioManager.Instance.musicSource.mute = true;
        }
    }

    public void SfxVolume()
    {
        AudioManager.Instance.SfxVolume(_sfxSlider.value);
        if (_sfxSlider.value > 0)
        {
            AudioManager.Instance.sfxSource.mute = false;
        }

        if (_sfxSlider.value == 0)
        {
            AudioManager.Instance.sfxSource.mute = true;
        }
    }

    public void OpenShopPanel()
    {
        isShop = true;
        AudioManager.Instance.PlaySFX("_button");
        ShopPanel.SetActive(true);
    }

    public void CloseShopPanel()
    {
        isShop = false;
        AudioManager.Instance.PlaySFX("_button");
        ShopPanel.SetActive(false);
    }

    public void BuyHammer()
    {
        AudioManager.Instance.PlaySFX("_button");
        if (numTotalCoin >= 1000)
        {
            numTotalCoin -= 1000;
            PlayerPrefs.SetInt("totalCoin", numTotalCoin);
            PlayerPrefs.SetInt("numHammer", 5);
            CheckInfo();
        }
    }

    public void BuyX2()
    {
        AudioManager.Instance.PlaySFX("_button");
        if (numTotalCoin >= 1000)
        {
            numTotalCoin -= 1000;
            PlayerPrefs.SetInt("totalCoin", numTotalCoin);
            PlayerPrefs.SetInt("numX2", 5);
            CheckInfo();
        }
    }

    public void ClickRoll()
    {
        if (rollCount <= 0)
        {
            return;
        }

        var listPos = new List<Vector3>();

        var count = fruits.Count;

        for (int i = count - 1; i >= 0; i--)
        {
            var f = fruits[i];

            listPos.Add(f.transform.position);
            RemoveFruit(f);
        }

        for (int i = 0; i < listPos.Count - 1; i++)
        {
            SpawnRandomFruit(listPos[i]);
        }
        
        fruit = SpawnNextFruit();

        _gameData.playerData.MinusNumberSkill((int) SkillType.Roll, 1);
        CheckInfo();
    }

    private void SpawnRandomFruit(Vector3 transformPosition)
    {
        var rand = Random.Range(0, 6);
        var prefab = fruitPrefabList[rand].gameObject;

        var f = SpawnFruit(prefab, transformPosition);
        f.SetSimulated(true);
        f.PlayFx();
    }

    public void ClickDestroy()
    {
        if (destroyCount <= 0)
        {
            return;
        }
        
        if (fruits == null || fruits.Count == 0)
        {
            return;
        }

        fruits.Shuffle();

        var count = Mathf.Min(5, fruits.Count);

        for (int i = count - 1; i >= 0; i--)
        {
            var f = fruits[i];
            f.PlayFx();
            RemoveFruit(f);
        }

        fruit = SpawnNextFruit();
        
        _gameData.playerData.MinusNumberSkill((int) SkillType.Destroy, 1);
        CheckInfo();
    }

    public void BackToHome()
    {
        SceneLoader.Instance.LoadHome();
    }
}