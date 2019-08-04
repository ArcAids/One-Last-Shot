using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementSelected : MonoBehaviour, IElemental
{
    [SerializeField] ElementalEventController elementEvent;
    [SerializeField] Image fireImage;
    [SerializeField] Image slashImage;
    [SerializeField] Image iceImage;

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
        switch (element)
        {
            case Elements.Fire:
                fireImage.enabled = true;
                break;
            case Elements.Ice:
                fireImage.enabled = true;
                break;
            case Elements.Slash:
                break;
            default:
                break;
        }
    }
}
