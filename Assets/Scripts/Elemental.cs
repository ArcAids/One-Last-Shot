using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elemental : MonoBehaviour,  IElemental
{
    [SerializeField] ElementalEventController elementController;
    [SerializeField] UnityEvent<Elements> onSwitch;
    public Elements Element { get; private set; }

    public void SwitchElement(Elements element)
    {
        Element = element;
        onSwitch.Invoke(element);
    }


    private void OnEnable()
    {
        elementController.RegisterElmentSwitch(this);
    }

    private void OnDisable()
    {
        elementController.DeregisterElmentSwitch(this);
    }

}
