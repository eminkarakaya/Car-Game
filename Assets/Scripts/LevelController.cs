using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class LevelController : MonoBehaviour
{
    public static event System.Action OnStart;
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject startTouch, tapToPlay;
    [SerializeField] private CarController[] carsInGame;
    [SerializeField] private TextMeshProUGUI numberOfText;
    public List<float> carsList;
    private bool _isStart;
    public bool isStart { get => _isStart;
        set 
        {
            var old = _isStart;
            _isStart = value;
            if(_isStart != old && _isStart)
                OnStart?.Invoke();
        }
    }
    [SerializeField] private GameObject[] cars;
    [SerializeField] private SelectedCar selectedCar;
    [SerializeField] private GameObject finishMenu;

    private void Awake()
    {
        var obj = Instantiate(cars[selectedCar.selectedCar], Vector3.up, Quaternion.identity);
    }
    private void Start()
    {
        carsInGame = FindObjectsOfType<CarController>();
    }
    private void OnEnable()
    {
        CarController.OnFinish += Finish;
    }
    private void OnDisable()
    {   
        CarController.OnFinish -= Finish;
    }
    private void Update()
    {
        ArabaSiralamasi();
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isStart = true;
                startTouch.SetActive(false);
                tapToPlay.SetActive(false);
            }
        }
    }
    private void ArabaSiralamasi()
    {
        carsList = carsInGame.Select(x => x.transform.position.z).ToList();
        //for (int i = 0; i < carsInGame.Length; i++)
        //{

        //}
        carsInGame = Sirala(carsList.ToArray(), carsInGame.ToArray());
        System.Array.Reverse(carsInGame);
        for (int i = 0; i < carsInGame.Length; i++)
        {
            carsInGame[i].numberOf = i;
        }
    }
    void Swap(float x, float y)
    {
        float temp;
        temp = x;
        x = y;
        y = temp;
    }
    public CarController[] Sirala(float[] arr, CarController[] carcontroller)
    {
        int min;
        for (int i = 0; i < arr.Length; i++)
        {
            min = i;
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[min] > arr[j])
                {
                    min = j;
                }
            }
            CarController temp2;
            temp2 = carcontroller[i];
            carcontroller[i] = carcontroller[min];
            carcontroller[min] = temp2;

            float temp;
            temp = arr[i];
            arr[i] = arr[min];
            arr[min] = temp;
            //Swap(arr[min], i);
        }
        return carcontroller;
    }
    public void StopBtn()
    {
        Time.timeScale = 0;
    }
    public void ContinueBtn()
    {
        Time.timeScale = 1;
    }
    public void RestartBtn()
    {
        SceneManager.LoadScene(gameObject.scene.buildIndex);
        ContinueBtn();
    }
    public void GarageBtn()
    {
        ContinueBtn();
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        levelData.level++;
        
        if(SceneManager.sceneCountInBuildSettings <= levelData.level)
        {
            levelData.level = 1;
        }
        SceneManager.LoadScene(levelData.level);
    }
    void Finish()
    {
        finishMenu.SetActive(true);
    }
    
}
