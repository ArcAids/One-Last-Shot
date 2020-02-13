using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBag : MonoBehaviour
{
    [SerializeField] ParticleSystem blood;
    [SerializeField] Transform bloodPivot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IShootable>()!=null)
        {
            bloodPivot.transform.parent = null;
            bloodPivot.right=collision.transform.right;
            blood.Play();
        }
    }
}
