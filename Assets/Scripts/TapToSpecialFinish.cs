using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;   
using DG.Tweening;
public class TapToSpecialFinish : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tapToSpecialFinish;
    [SerializeField] private float dur,destroyDur = 2f,scaleMultiplier;
    CarController carController;
    float temp;
    private void Start()
    {
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponent<Collider>().enabled = false;
            carController.specialFinish = true;
            tapToSpecialFinish.gameObject.SetActive(true);
            tapToSpecialFinish.transform.DOScale( Vector3.one * scaleMultiplier, dur).OnComplete(()=> tapToSpecialFinish.transform.DOScale(Vector3.one, dur)).SetLoops(-1);
            StartCoroutine(False());
        }
    }
    IEnumerator False()
    {
        temp = destroyDur;
        while(true)
        {
            temp -= Time.deltaTime;
            if(!carController.specialFinish)
            {
                carController.specialFinish = false;
                Destroy(tapToSpecialFinish.gameObject);
                Destroy(this.gameObject);
            }
            if (temp < 0)
            {
                break;
            }
                
            yield return null;
        }
        carController.specialFinish = false;
        Destroy(tapToSpecialFinish.gameObject);
        Destroy(this.gameObject);
    }
    
}
