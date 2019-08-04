using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float xPercentage;
    [SerializeField] float yPercentage;
    [SerializeField] iTween.EaseType easeType;
    [SerializeField] iTween.LoopType loopType;


    // Start is called before the first frame update
    void Start()
    {
        iTween.ScaleTo(gameObject,iTween.Hash("y", transform.localScale.y *yPercentage, "x", transform.localScale.x * xPercentage,"EaseType", easeType,"LoopType",loopType,"speed",speed));
    }

}
