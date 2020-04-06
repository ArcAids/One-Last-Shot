using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TweenScale : MonoBehaviour
{
    [SerializeField] bool startOnAwake = true;
    [SerializeField] float speed;
    [SerializeField] public float xPercentage;
    [SerializeField] public float yPercentage;
    [SerializeField] iTween.LoopType loopType;
    [SerializeField] iTween.EaseType easeType;
    [SerializeField] Ease ease=Ease.InSine;
    [SerializeField] bool loops = true;
    [SerializeField] LoopType loopingType;


    // Start is called before the first frame update
    void Start()
    {
        if (startOnAwake) StartTween();
    }

    public void StartTween()
    {
        Vector3 scaleTo = new Vector3(transform.localScale.x * xPercentage, transform.localScale.y * yPercentage, transform.localScale.z);
        Tween tween=transform.DOScale(scaleTo,speed).SetEase(ease).SetSpeedBased();
        if (loops)
            tween.SetLoops(-1, loopingType);
        //iTween.ScaleTo(gameObject, iTween.Hash("y", transform.localScale.y * yPercentage, "x", transform.localScale.x * xPercentage, "EaseType", easeType, "LoopType", loopType, "speed", speed));
    }
}
