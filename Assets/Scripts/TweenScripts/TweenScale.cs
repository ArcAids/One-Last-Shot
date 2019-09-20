using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale : MonoBehaviour
{
    [SerializeField] bool startOnAwake = true;
    [SerializeField] float speed;
    [SerializeField] public float xPercentage;
    [SerializeField] public float yPercentage;
    [SerializeField] iTween.EaseType easeType;
    [SerializeField] iTween.LoopType loopType;


    // Start is called before the first frame update
    void Start()
    {
        if (startOnAwake) StartTween();
    }

    public void StartTween()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("y", transform.localScale.y * yPercentage, "x", transform.localScale.x * xPercentage, "EaseType", easeType, "LoopType", loopType, "speed", speed));
    }
}
