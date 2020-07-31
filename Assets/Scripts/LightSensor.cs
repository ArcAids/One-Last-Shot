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

    private void Start()
    {
        if (DayNightCycler2D.DNCycler != null)
        { 
            DayNightCycler2D.DNCycler.RegisterLight(this);
            
        }
    }

    private void OnEnable()
    {
        if (DayNightCycler2D.DNCycler != null)
        {
            DayNightCycler2D.DNCycler.RegisterLight(this);
        }
    }

    private void OnDisable()
    {
        if(DayNightCycler2D.DNCycler!=null)
            DayNightCycler2D.DNCycler.UnregisterLight(this);
    }

    public void UpdateLight(float sunIntensityRatio)
    {
        if(connectedLight!=null)
        {
            if (only2States){
                if (sunIntensityRatio < switchThreshold)
                    connectedLight.enabled = true;
                else
                    connectedLight.enabled = false;
            }
            else
            {
                connectedLight.enabled = true;
                connectedLight.intensity = CalculateIntensity(sunIntensityRatio);
            }
        }

    }

    float CalculateIntensity(float targetIntensityRatio)
    {
        float intensity =intensityRange * (1-targetIntensityRatio);
        intensity += minIntensity;
        return Mathf.Max(0, intensity);
    }
}
