using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class RankingListManager : MonoBehaviour
{
    public static RankingListManager instance;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject userPrefab;
    [SerializeField] private FlagData flagData;
    [SerializeField] private Vector2Int goldRangeMinMax;
    [SerializeField] private int currentRank;
    [SerializeField] private Vector2Int fromTo;
    [SerializeField] private UserList root = new UserList();
    private int tempRank;
    private void Awake()
    {
        instance = this;
        string path = "UserData.json";
        string filePath = path.Replace(".json", "");

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        root = JsonUtility.FromJson<UserList>(targetFile.text);
    }
    private void Start()
    {
        tempRank = fromTo.x;
        for (int i = 0; i < /*fromTo.y - fromTo.x +*/ 20 ; i++)
        {
            CreateUser();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Scrolling();
        }
    }
    public void RandomData(User user)
    {
        user.flag = flagData.flags[Random.Range(0, flagData.flags.Count)];
        user.nick = root.users[Random.Range(0, root.users.Count)].nick;
        user.gold = Random.Range(goldRangeMinMax.x, goldRangeMinMax.y);
    }
    public void CreateUser()
    {
        var user = Instantiate(userPrefab, Vector3.zero, Quaternion.identity, contentParent);
        user.GetComponent<UserData>().SetRank(tempRank);
        tempRank++;
    }
    public void Scrolling()
    {
        DOTween.To(() => scrollRect.verticalNormalizedPosition, x => scrollRect.verticalNormalizedPosition = x, .5f, 1f);       
        //scrollRect.verticalNormalizedPosition = 1; // value range (0 to 1)
    }
}
