using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElementalEventController : ScriptableObject
{
    [SerializeField] Elements currentElement;

    List<IElemental> elementals;

    public Elements CurrentElement { get => currentElement; private set { Switch(value); } }

    public void RegisterElmentSwitch(IElemental elemental)
    {
        if(elementals==null)
            elementals = new List<IElemental>();
        if (!elementals.Contains(elemental))
            elementals.Add(elemental);
    }

    public void DeregisterElmentSwitch(IElemental elemental)
    {
        elementals.Remove(elemental);
    }

    public void Switch(Elements element)
    {
        currentElement = element;
        
        foreach (var elemental in elementals)
        {
            elemental.SwitchElement(element);
        }
    }
}


public enum Elements
{
    Fire, Ice, Slash
}
