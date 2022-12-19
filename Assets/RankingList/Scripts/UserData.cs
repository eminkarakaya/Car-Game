using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UserData : MonoBehaviour
{
    User user = new User();
    [Header("UI")]
    
    [SerializeField] private TextMeshProUGUI nickText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Image flagImage;
    private void Awake()
    {
        RankingListManager.instance.RandomData(user);
        goldText.text = user.gold.ToString();
        nickText.text = user.nick.ToString();
        flagImage.sprite = user.flag;
    }
    public void SetRank(int value)
    {
        user.rank = value;
        rankText.text = "#"+ value.ToString();
    }
}
