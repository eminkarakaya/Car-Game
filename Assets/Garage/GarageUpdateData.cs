using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class GarageUpdateData : MonoBehaviour
{
    [SerializeField] private Material[] carMaterials;
    [SerializeField] private Material lockMaterial;
    // input
    private Vector2 startPos;
    private int pixelDistToDetect = 100;
    private bool fingerDown;
    //
    [SerializeField] private LevelData levelData;
    [Header("UI")]
    
    [SerializeField] private TextMeshProUGUI carNameText;
    [SerializeField] private Slider speedSlider, boostedSpeedSlider, horizontalSpeedSlider, accelerationSpeedSlider;
    [SerializeField] private GameObject selectedImage,lockImage;
    [SerializeField] private GameObject selectButton, goButton, buyButton,rightArrow,leftArrow;

    [Header("UI Data")]
    [SerializeField] private float speedMaxValue;
    [SerializeField] private float boostedSpeedMaxValue, horizontalSpeedMaxValue, accelerationSpeedMaxValue;
    [Space(20)]
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private SelectedCar selectedCar;
    [SerializeField] private float spaceBetweenCars;
    [SerializeField] private float animationDuration;
    [SerializeField] private Transform cameraAndLight;
    [SerializeField] private CarData [] data;
    [SerializeField] private CarLockData[] lockData;
    [SerializeField] private int currentCar = 0;
    [SerializeField] private GameObject costImageText;

    private void Start()
    {
        speedSlider.maxValue = speedMaxValue;
        boostedSpeedSlider.maxValue = boostedSpeedMaxValue;
        horizontalSpeedSlider.maxValue = horizontalSpeedMaxValue;
        accelerationSpeedSlider.maxValue = accelerationSpeedMaxValue;
        for (int i = 0; i < lockData.Length; i++)
        {
            if (!data[i].isLocked)
            {
                lockData[i].lockedObject.SetActive(false);
                lockData[i].unlockedObject.SetActive(true);
            }
            else
            {
                lockData[i].lockedObject.SetActive(true);
                lockData[i].unlockedObject.SetActive(false);
            }
        }
        UpdateCarData();
        if (selectedCar.selectedCar == currentCar)
        {
            selectButton.SetActive(false);
            goButton.SetActive(true);
            selectedImage.SetActive(true);
        }
        else
        {
            selectButton.SetActive(true);
            goButton.SetActive(false);
            selectedImage.SetActive(false);
        }
        CheckArrow();
        CheckGold();
        CheckLock();
    }
    private void Update()
    {
        if (Input.touchCount == 0)
            return;
        if(fingerDown == false)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                fingerDown = true;
            }

        }
        if (fingerDown)
        {
            if(Input.GetTouch(0).position.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                // left swipe
                Left();
            }
            else if (Input.GetTouch(0).position.x <= startPos.x - pixelDistToDetect)
            {
                // right swipe
                fingerDown = false;
                Right();
            }
        }
    }
    public void GOButton()
    {
        SceneManager.LoadScene(levelData.level);
    }
    public void SelectCar()
    {
        selectedCar.selectedCar = currentCar;
        selectButton.SetActive(false);
        goButton.SetActive(true);
        selectedImage.SetActive(true);
    }
    public void CheckLock()
    {
        if(data[currentCar].isLocked)
        {
            lockImage.SetActive(true);
        }
        else
        {
            lockImage.SetActive(false);
        }
    }
    public void UpdateCarData()
    {
        carNameText.text = data[currentCar].carName;
        DOTween.To(()=> speedSlider.value,x=> speedSlider.value = x, data[currentCar].maxSpeed, .3f);
        DOTween.To(()=> boostedSpeedSlider.value, x=> boostedSpeedSlider.value = x, data[currentCar].boostedMaxSpeed, .3f);
        DOTween.To(()=> horizontalSpeedSlider.value,x=> horizontalSpeedSlider.value = x, data[currentCar].horizontalSpeed, .3f);
        DOTween.To(()=> accelerationSpeedSlider.value,x=> accelerationSpeedSlider.value = x, data[currentCar].accelerationSpeed, .3f);
        //speedSlider.value = data[currentCar].maxSpeed;
        //boostedSpeedSlider.value = data[currentCar].boostedMaxSpeed;
        //horizontalSpeedSlider.value = data[currentCar].horizontalSpeed;
        //accelerationSpeedSlider.value = data[currentCar].accelerationSpeed;
    }
    private void Left()
    {
        if (currentCar > 0)
        {
            currentCar--;
            CheckArrow();
            CheckGold();
            CheckLock();
            cameraAndLight.transform.DOMoveX(-currentCar * spaceBetweenCars, animationDuration);
            UpdateCarData();
            if(data[currentCar].isLocked)
            {
                buyButton.SetActive(true);
                selectButton.SetActive(false);
                goButton.SetActive(false);
                selectedImage.SetActive(false);
                return;
            }
            CheckSelected();
        }
    }
    private void Right()
    {
        if(currentCar < data.Length-1)
        {
            currentCar++;
            CheckArrow();
            CheckGold();
            CheckLock();

            cameraAndLight.transform.DOMoveX(-currentCar * spaceBetweenCars, animationDuration);
            UpdateCarData();
            if (data[currentCar].isLocked)
            {
                buyButton.SetActive(true);
                selectButton.SetActive(false);
                goButton.SetActive(false);
                selectedImage.SetActive(false);
                return;
            }
            CheckSelected();
        }
    }
    public void CheckSelected()
    {
        if (selectedCar.selectedCar == currentCar)
        {
            buyButton.SetActive(false);
            selectButton.SetActive(false);
            goButton.SetActive(true);
            selectedImage.SetActive(true);
        }
        else
        {
            buyButton.SetActive(false);
            selectButton.SetActive(true);
            goButton.SetActive(false);
            selectedImage.SetActive(false);
        }
    }
    public void Unlock()
    {
        if (data[currentCar].cost > GameManager.instance.GetGold())
            return;
        GameManager.instance.SetGold(data[currentCar].cost);
        data[currentCar].isLocked = false;
        lockData[currentCar].unlockedObject.SetActive(true);
        lockData[currentCar].lockedObject.SetActive(false);
        lockImage.SetActive(false);
        CheckSelected();
        CheckLock();
        CheckGold();
    }
    void CheckArrow()
    {
        if (currentCar < data.Length - 1)
        {
            rightArrow.SetActive(true);
        }
        else rightArrow.SetActive(false);
        if (currentCar > 0)
        {
            leftArrow.SetActive(true);
        } else leftArrow.SetActive(false);
    }
    public void CheckGold()
    {
        if (!data[currentCar].isLocked)
        {
            buyButton.SetActive(false);
            costImageText.SetActive(false);
            return;
        }
        else
        {
            buyButton.SetActive(true);
            costImageText.SetActive(true);
            costText.text = data[currentCar].cost.ToString();
        }
        if (data[currentCar].cost > GameManager.instance.GetGold())
        {
            buyButton.GetComponent<Image>().color = Color.gray;
            costText.color = Color.red;
        }
        else
        {
            buyButton.GetComponent<Image>().color = Color.green;
            costText.color = Color.black;
        }

    }
[System.Serializable] 
public struct CarLockData
{
    public GameObject unlockedObject, lockedObject;
}
}
