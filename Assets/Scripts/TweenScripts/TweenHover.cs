using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenHover : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float xDirection;
    [SerializeField] float yDirection;
    [SerializeField] iTween.EaseType easeType;
    [SerializeField] iTween.LoopType loopType;


    // Start is called before the first frame update
    void Start()
    {
        iTween.MoveAdd(gameObject,iTween.Hash("y",yDirection, "x", xDirection,"EaseType", easeType,"LoopType",loopType,"speed",speed));
    }

}
