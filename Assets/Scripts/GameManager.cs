using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour , IDataPersistence
{
    
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private int _gold;
    
    private void Awake()
    {
        GameManager[] managers = FindObjectsOfType<GameManager>();

        if (managers.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }
    private void Start()
    {
        SetGold(0);
    }
    public int GetGold()
    {
        return _gold;
    }
    public void SetGold(int gold)
    {
        _gold += gold;
        _goldText.text = _gold.ToString();
    }

    public void LoadData(GameData data)
    {
        _gold = data.gold;
        Debug.Log("gold" + _gold);
    }

    public void SaveData(GameData data)
    {
        data.gold = _gold;
    }
    public int GetMoney()
    {
        return _gold;
    }
    public static string CaclText(float value)
    {
        if (value == 0)
        {
            return "0";
        }
        if (value < 1000)
        {
            return String.Format("{0:0.0}", value);
        }
        else if (value >= 1000 && value < 1000000)
        {
            return String.Format("{0:0.0}", value / 1000) + "k";
        }
        else if (value >= 1000000 && value < 1000000000)
        {
            return String.Format("{0:0.0}", value / 1000000) + "m";
        }
        else if (value >= 1000000000 && value < 1000000000000)
        {
            return String.Format("{0:0.0}", value / 1000000000) + "b";
        }
        else if (value >= 1000000000000 && value < 1000000000000000)
        {
            return String.Format("{0:0.0}", value / 1000000000000) + "t";
        }
        else if (value >= 1000000000000000 && value < 1000000000000000000)
        {
            return String.Format("{0:0.0}", value / 1000000000000000) + "aa";
        }
        else if (value >= 1000000000000000000)
        {
            return String.Format("{0:0.0}", value / 1000000000000000) + "ab";
        }
        return value.ToString();
    }
}

