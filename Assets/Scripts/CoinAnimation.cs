using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CoinAnimation : MonoBehaviour
{
    Vector3 rot = new Vector3(0, 180, 0);
    [SerializeField] private Ease ease;
    Sequence seq;
    void Start()
    {
        seq = DOTween.Sequence();
        transform.DORotate(rot, 3f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        //seq.Append(transform.DOMoveY(2, 1f).SetEase(ease)).SetLoops(-1, LoopType.Yoyo);
        //seq.Append(transform.DOMoveY(1, 1f).SetEase(ease)).SetLoops(-1, LoopType.Yoyo);
        //seq.SetLoops(-1,LoopType.Yoyo);
        transform.DOMoveY(transform.position.y + 1, 1f).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
            //OnComplete(() => transform.DOMoveY(transform.position.y - 1, 1f).SetEase(ease))/*.SetLoops(-1)*/;/*.SetLoops(-1).SetEase(ease);*/
    }
}
