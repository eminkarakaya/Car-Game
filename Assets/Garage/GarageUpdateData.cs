using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class GarageUpdateData : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI carNameText;
    [SerializeField] private Slider speedSlider, boostedSpeedSlider, horizontalSpeedSlider, accelerationSpeedSlider;
    [SerializeField] private GameObject selectedImage;
    [SerializeField] private GameObject selectButton,goButton;

    [Header("UI Data")]
    [SerializeField] private float speedMaxValue;
    [SerializeField] private float boostedSpeedMaxValue, horizontalSpeedMaxValue, accelerationSpeedMaxValue;
    [Space(20)]

    [SerializeField] private SelectedCar selectedCar;
    [SerializeField] private float spaceBetweenCars;
    [SerializeField] private float animationDuration;
    [SerializeField] private Transform cameraAndLight;
    [SerializeField] private CarData [] data;
    [SerializeField] private int currentCar = 0;
    private void Start()
    {
        speedSlider.maxValue = speedMaxValue;
        boostedSpeedSlider.maxValue = boostedSpeedMaxValue;
        horizontalSpeedSlider.maxValue = horizontalSpeedMaxValue;
        accelerationSpeedSlider.maxValue = accelerationSpeedMaxValue;

        UpdateCarData();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Left();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            Right();
        }
    }
    public void GOButton()
    {
        SceneManager.LoadScene(1);
    }
    public void SelectCar()
    {
        selectedCar.selectedCar = currentCar;
        selectButton.SetActive(false);
        goButton.SetActive(true);
        selectedImage.SetActive(true);
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
        if(currentCar > 0)
        {
            currentCar--;
            cameraAndLight.transform.DOMoveX(-currentCar * spaceBetweenCars, animationDuration);
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
        }
    }
    private void Right()
    {
        if(currentCar < data.Length-1)
        {

            currentCar++;
            cameraAndLight.transform.DOMoveX(-currentCar * spaceBetweenCars, animationDuration);
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
        }
    }
}
