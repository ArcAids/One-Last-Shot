using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementSelected : MonoBehaviour, IElemental
{
    [SerializeField] ElementalEventController elementEvent;
    [SerializeField] Animator animator;
    [SerializeField] float speed;

    float target;
    float currentValue;
    public Elements Element
    {
        get; set;
    }


    private void OnEnable()
    {
        elementEvent.RegisterElmentSwitch(this);
    }

    private void OnDisable()
    {
        elementEvent.DeregisterElmentSwitch(this);
    }

    public void SwitchElement(Elements element)
    {
        target = (int)element + 1;
    }

    private void FixedUpdate()
    {
        currentValue = Mathf.Lerp(currentValue,target,Time.fixedDeltaTime *speed);
        animator.SetFloat("Blend", currentValue);
    }
}
