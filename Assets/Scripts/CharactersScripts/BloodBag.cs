using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBag : MonoBehaviour
{
    [SerializeField] ParticleSystem blood;
    [SerializeField] Transform bloodPivot;
    private void Start()
    {
        
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.GetComponent<IShootable>()!=null)
    //    {
    //        bloodPivot.right = collision.transform.right;
    //        Bleed();
    //    }
    //}

    public void Bleed()
    {
        bloodPivot.transform.parent = null;
        blood.Play();
    }
}
