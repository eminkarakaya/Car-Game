using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startTouch;
    public bool isStart;
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private int _gold,selectedCar;
    [SerializeField] private GameObject[] cars;

    private void Awake()
    {
        GameManager[] managers = GameObject.FindObjectsOfType<GameManager>();

        if (managers.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }
    
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            isStart = true;
            startTouch.SetActive(false);
        }
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
