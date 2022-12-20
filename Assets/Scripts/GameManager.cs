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
    [SerializeField] private TextMeshProUGUI _fuelText;
    [SerializeField] private int _fuel;
    
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
        SetFuel(0);
    }
    public int GetFuel()
    {
        return _fuel;
    }
    public void SetFuel(int value)
    {
        _fuel += value;
        _fuelText.text = _fuel.ToString();
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
        _fuel = data.fuel;
    }

    public void SaveData(GameData data)
    {
        data.gold = _gold;
        data.fuel = _fuel;
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
