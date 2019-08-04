using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    [SerializeField] ElementalEventController eventController;
    IElementControlInput input;

    private void Awake()
    {
        input = GetComponent<IElementControlInput>();
    }

    private void Update()
    {
        input.SetInputs();
        if(input.ActivateFireElement)
            eventController.Switch(Elements.Fire);
        else if (input.ActivateIceElement)
            eventController.Switch(Elements.Ice);
        else if (input.ActivateSlashElement)
            eventController.Switch(Elements.Slash);
    }
}
