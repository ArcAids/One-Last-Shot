using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ElementalUtility
{
    
    public static Color GetColor(Elements element)
    {
        switch (element)
        {
            case Elements.Fire:
                return Color.red;

            case Elements.Ice:
                return Color.cyan;

            case Elements.Slash:
               return Color.white;

            default:
               return Color.white;

        }
    }


}
