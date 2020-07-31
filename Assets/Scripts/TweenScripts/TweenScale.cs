using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TweenScale : MonoBehaviour
{
    [SerializeField] bool startOnAwake = true;
    [SerializeField] float duration;
    [SerializeField] float delay=0;
    [SerializeField] public float xPercentage;
    [SerializeField] public float yPercentage;
    [SerializeField] Ease ease=Ease.InSine;
    [SerializeField] bool loops = true;
    [SerializeField] LoopType loopingType;

    Vector3? originalScale;
    Tween tween;
    void Awake()
    {
        originalScale = transform.localScale;
    }
   
    void OnEnable()
    {
        if (startOnAwake)
            StartTween();
    }
   
    public void StartTween()
    {
        if(!originalScale.HasValue)
            originalScale=transform.localScale;

        if(tween!=null)
            tween.Complete();
        StartTween(duration,delay);
    }
    public void StartTween(float duration)
    {
        StartTween(duration, delay);
    }

    public void Rewind()
    {
        if(tween!=null)
            tween.Kill();

        transform.DOScale(originalScale.Value, duration).SetDelay(delay).SetEase(ease);
    }

    public void StartTween(float duration,float delay)
    {
        transform.localScale = originalScale.Value;

        Vector3 scaleTo = new Vector3(transform.localScale.x * xPercentage, transform.localScale.y * yPercentage, transform.localScale.z);
        tween = transform.DOScale(scaleTo, duration).SetDelay(delay).SetEase(ease);
        if (loops)
            tween.SetLoops(-1, loopingType);
    }
}
