using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject startTouch, tapToPlay;
    public bool isStart;
    [SerializeField] private GameObject[] cars;
    [SerializeField] private SelectedCar selectedCar;
    [SerializeField] private GameObject finishMenu;

    private void Awake()
    {
        var obj = Instantiate(cars[selectedCar.selectedCar], Vector3.up, Quaternion.identity);
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
