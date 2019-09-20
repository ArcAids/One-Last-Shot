using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    [SerializeField] ElementalEventController eventController;
    IElementControlInput input;
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem ice;
    [SerializeField] ParticleSystem magic;
    Elements currentElement;
    private void Awake()
    {
        input = GetComponent<IElementControlInput>();
    }
    private void Start()
    {
        currentElement = Elements.Ice;
        ActivateElement(Elements.Fire);
    }
    private void Update()
    {
        input.SetInputs();
        if (input.ActivateFireElement)
        {
            ActivateElement(Elements.Fire);
        }
        else if (input.ActivateIceElement)
        {
            ActivateElement(Elements.Ice);
        }
        else if (input.ActivateSlashElement)
        {
            ActivateElement(Elements.Slash);
        }
    }

    void ActivateElement(Elements element)
    {
        if(currentElement == element)
            return;
        currentElement = element;
        eventController.Switch(element);
        switch (element)
        {
            case Elements.Fire:
                fire.gameObject.SetActive(true);
                ice.gameObject.SetActive(false);
                magic.gameObject.SetActive(false);
                break;
            case Elements.Ice:
                fire.gameObject.SetActive(false);
                ice.gameObject.SetActive(true);
                magic.gameObject.SetActive(false);
                break;
            case Elements.Slash:
                fire.gameObject.SetActive(false);
                ice.gameObject.SetActive(false);
                magic.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
