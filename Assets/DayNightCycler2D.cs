using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
[ExecuteInEditMode]
public class DayNightCycler2D : MonoBehaviour
{
    [SerializeField] UnityEngine.Experimental.Rendering.Universal.Light2D sun;

    [SerializeField] float nightIntensity=0;
    [SerializeField] float dayIntensity=1;

    static List<ISenseLight> activeLights;

    float currentSunIntensity;

    public static DayNightCycler2D DNCycler;

    private void Awake()
    {
        if(sun==null)
            sun = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        currentSunIntensity = sun.intensity-1;

        

        if (DNCycler == null)
            DNCycler = this;
        else
            Destroy(this);
    }

    public static void RegisterLight(ISenseLight light)
    {
        if (activeLights == null)
            activeLights = new List<ISenseLight>();
        if (!activeLights.Contains(light))
            activeLights.Add(light);
    }
    public static void UnregisterLight(ISenseLight light)
    {
        if (activeLights.Contains(light))
            activeLights.Remove(light);
    }

    
    private void FixedUpdate()
    {
        if(currentSunIntensity!=sun.intensity)
        {
            currentSunIntensity = sun.intensity;
            UpdateLights(currentSunIntensity);
        }
    }

    public void UpdateSun(float intensity)
    {
        StartCoroutine(GraduallyChangeSunIntensity(intensity));
    }

    IEnumerator GraduallyChangeSunIntensity(float targetIntensity)
    {
        while(sun.intensity!=targetIntensity)
        {
            sun.intensity = Mathf.MoveTowards(sun.intensity,targetIntensity,Time.deltaTime *0.5f);
            yield return null;
        }
    }

    void UpdateLights(float sunIntensity)
    {
        foreach (var activeLight in activeLights)
        {
            activeLight.UpdateLight(GetSunIntensityRatio(sunIntensity));
        }
    }

    float GetSunIntensityRatio(float sunIntensity)
    {
        return sunIntensity / (dayIntensity - nightIntensity);
    }
}
