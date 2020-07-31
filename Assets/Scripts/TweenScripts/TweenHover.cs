﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenHover : MonoBehaviour
{
    [SerializeField] float duration=1;
    [SerializeField] float delay=0;
    [SerializeField] float xDirection=0;
    [SerializeField] float yDirection=0;
    [SerializeField] Ease ease = Ease.InSine;
    [SerializeField] bool loops = true;
    [SerializeField] LoopType loopingType;

    // Start is called before the first frame update
    void Start()
    {
        //iTween.MoveAdd(gameObject,iTween.Hash("y",yDirection, "x", xDirection,"EaseType", easeType,"LoopType",loopType,"speed",speed));
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        yield return new WaitForSeconds(delay);
        Vector3 hoverTo = new Vector3(transform.localPosition.x + xDirection, transform.localPosition.y + yDirection, transform.localPosition.z);
        Tween tween = transform.DOLocalMove(hoverTo, duration).SetEase(ease);

        if (loops)
            tween.SetLoops(-1, loopingType);
    }
}
