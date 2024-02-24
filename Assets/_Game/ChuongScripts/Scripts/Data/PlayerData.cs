using System;
using System.Collections.Generic;
using System.Linq;
using ChuongCustom;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerData : BaseData
{
    public int _coin, _gem, _exp;

    public int choosingMap;
    public List<MapRecord> mapUnlocks;
    public float time;
    public string timeRegister;

    public bool isRate;

    //donut game data 
    public List<int> unlockedBGs;
    public List<int> unlockedSkins;

    public int choosingBG;
    public int choosingSkin;

    private int[] numberSkill;

    public int GetNumberSkill(int ID)
    {
        return numberSkill[ID];
    }

    public void AddNumberSkill(int ID, int amount)
    {
        numberSkill[ID] += amount;

        Save();
    }

    public void MinusNumberSkill(int ID, int amount)
    {
        numberSkill[ID] -= amount;

        Save();
    }

    public int Coin
    {
        get => _coin;
        set
        {
            _coin = value;
            GameAction.OnChangeCoin?.Invoke(value);
        }
    }

    public int Gem
    {
        get => _gem;
        set
        {
            _gem = value;
            GameAction.OnChangeGem?.Invoke(value);
        }
    }

    public int Exp
    {
        get => _exp;
        set
        {
            _exp = value;
            GameAction.OnChangeExp?.Invoke(value);
        }
    }

    public void Rated()
    {
        isRate = true;
    }

    public bool IsRate => isRate;

    public void ResetTime()
    {
        time = 0;
        Save();
    }

    public override void ValidateData()
    {
        if (mapUnlocks == null || mapUnlocks.Count == 0)
        {
            mapUnlocks = new List<MapRecord>();
            mapUnlocks.Add(new MapRecord()
            {
                mapID = 0,
                highScore = 0
            });
        }
    }

    public override void Init()
    {
        prefString = Constant.DataKey_PlayerData;
        if (PlayerPrefs.GetString(prefString).Equals(""))
        {
            ResetData();
        }

        base.Init();
    }


    public override void ResetData()
    {
        timeRegister = DateTime.Now.ToBinary().ToString();
        time = 3 * 24 * 60 * 60;

        _coin = 0;
        _gem = 100000;
        _exp = 0;

        mapUnlocks = new List<MapRecord>();
        mapUnlocks.Add(new MapRecord()
        {
            mapID = 0,
            highScore = 0
        });

        //donut data
        unlockedSkins = new List<int> {0};
        unlockedBGs = new List<int> {0};

        choosingSkin = 0;
        choosingMap = 0;
        numberSkill = new int[4];

        for (int i = 0; i < numberSkill.Length; i++)
        {
            numberSkill[i] = 2;
        }

        Save();
    }

    protected override void CheckAppendData()
    {
        base.CheckAppendData();

        if (unlockedSkins == null || unlockedSkins == null)
        {
            unlockedSkins = new List<int> {0};
            unlockedSkins = new List<int> {0};
        }
        
        if (numberSkill is {Length: >= 4}) return;

        numberSkill = new int[4];

        for (int i = 0; i < numberSkill.Length; i++)
        {
            numberSkill[i] = 2;
        }
        
      
    }

    public void ChooseSkin(int skinID)
    {
        choosingSkin = skinID;
    }

    public void AddSkin(int skinID)
    {
        if (!unlockedSkins.Contains(skinID))
        {
            unlockedSkins.Add(skinID);
        }
    }

    public bool CheckContainSkin(int skinID)
    {
        return unlockedSkins.Contains(skinID);
    }

    public void ChooseBG(int BGID)
    {
        choosingBG = BGID;
    }

    public void AddBG(int BGID)
    {
        if (!unlockedBGs.Contains(BGID))
        {
            unlockedBGs.Add(BGID);
        }
    }

    public bool CheckContainBG(int BGID)
    {
        return unlockedBGs.Contains(BGID);
    }

    public bool IsUnlockMap(int mapID)
    {
        var findMap = mapUnlocks.Find(record => record.mapID == mapID);

        return (findMap != null);
    }

    public void UnlockMap(int mapID)
    {
        if (!IsUnlockMap(mapID))
        {
            mapUnlocks.Add(new MapRecord()
            {
                mapID = mapID,
                highScore = 0
            });
            mapUnlocks = mapUnlocks.OrderBy(i => i.mapID).ToList();
        }

        Save();
    }

    public MapRecord GetMapWithID(int mapID)
    {
        var findMap = mapUnlocks.Find(record => record.mapID == mapID);

        return findMap;
    }

    public void SetMaxHighScore(int playTime, int chapter)
    {
        if (GetMapWithID(chapter).highScore > playTime)
            return;

        GetMapWithID(chapter).highScore = playTime;
    }
}

[Serializable]
public class MapRecord
{
    public int mapID;
    public int highScore;
}