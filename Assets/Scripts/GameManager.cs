using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
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
}
