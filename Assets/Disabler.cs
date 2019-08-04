using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    [SerializeField] bool startOnAwake;
    [SerializeField] bool alsoDestroy;
    [SerializeField] float disableAfter;

    private void Awake()
    {
        if(startOnAwake)
            DisableWithDelay();
    }


    public void DisableWithDelay()
    {
        Invoke("Disable", disableAfter);

    }

    void Disable()
    {
        gameObject.SetActive(false);
        if(alsoDestroy)
        Destroy(gameObject);
    }
}
