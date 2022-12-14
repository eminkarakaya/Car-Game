using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private int _gold;
    private void Awake()
    {
        instance = this;
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
}
