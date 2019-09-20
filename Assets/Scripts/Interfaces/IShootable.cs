using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    void Shoot();
}

public interface IElementalShootable: IShootable, IElemental
{
   
}