using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public interface ISenseLight
{
    void UpdateLight(float sunIntensity);
}

[ExecuteInEditMode]
public class LightSensor : MonoBehaviour , ISenseLight
{
    [SerializeField] bool only2States=false;
    [SerializeField] float switchThreshold=0.3f;
    [SerializeField] float minIntensity=0;
    float maxIntensity=1;


    private UnityEngine.Experimental.Rendering.Universal.Light2D connectedLight;
    float intensityRange;

    private void Awake()
    {
        connectedLight = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        maxIntensity = connectedLight.intensity;
        intensityRange = maxIntensity - minIntensity;
    }

    private void OnEnable()
    {
        DayNightCycler2D.RegisterLight(this);
    }

    private void OnDisable()
    {
        DayNightCycler2D.UnregisterLight(this);
    }

    public void UpdateLight(float sunIntensity)
    {
        if(connectedLight!=null)
        {
            if (only2States){
                if (sunIntensity < switchThreshold)
                    connectedLight.enabled = true;
                else
                    connectedLight.enabled = false;
            }
            else
                connectedLight.intensity = CalculateIntensity(1- sunIntensity);
        }

    }

    float CalculateIntensity(float targetIntensity)
    {
        float intensity =intensityRange * targetIntensity;
        intensity += minIntensity;
        return Mathf.Max(0, intensity);
    }
}
