using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngineInternal;

[ExecuteInEditMode]
public class DayNightCycler2D : MonoBehaviour
{
    [SerializeField] UnityEngine.Experimental.Rendering.Universal.Light2D sun;

    [SerializeField] float nightIntensity=0;
    [SerializeField] float dayIntensity=1;
    [Range(0,1)]
    [SerializeField] float dayTimeProportion=1;
    List<ISenseLight> activeLights;

    float time=0;
    float currentSunIntensity;
    float targetSunIntensity;
    float oldDTProportion=-1;

    public static DayNightCycler2D DNCycler;
    bool sunSetting=true;

    Coroutine changingTime;

    private void Awake()
    {
        SetUp();
    }

    private void SetUp()
    {
        if(sun==null)
            sun = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        currentSunIntensity = sun.intensity-1;

        //dayTimeProportion = GetSunIntensityRatio(currentSunIntensity);
        time = dayTimeProportion;
        activeLights = new List<ISenseLight>();

        if (DNCycler == null)
            DNCycler = this;
        else
            Destroy(this);
    }

    public void RegisterLight(ISenseLight light)
    {
        if (!activeLights.Contains(light))
        {
            activeLights.Add(light);
            
            light.UpdateLight(GetSunIntensityRatio(currentSunIntensity));
        }
    }

    public void UnregisterLight(ISenseLight light)
    {
        if (activeLights.Contains(light))
            activeLights.Remove(light);
    }
    
    private void FixedUpdate()
    {
        //if (Input.GetKeyUp(KeyCode.F))
        //    FlowDayTime(.2f);

        if (currentSunIntensity!=sun.intensity)
        {
            currentSunIntensity = sun.intensity;
            UpdateLights(GetSunIntensityRatio(currentSunIntensity));
        }
        else if (dayTimeProportion != oldDTProportion)
        {
            oldDTProportion = dayTimeProportion;
            SetTime(dayTimeProportion);
        }

    }

    public void FlowDayTime(float duration)
    {
        time += duration;
        dayTimeProportion= Mathf.Abs(Mathf.Sin((time)*.5f * Mathf.PI));
    }

    public void PassTime(float dayLengthPercentage)
    {
        float currentSunIntensity = sun.intensity;
        float intensityOverTime = dayLengthPercentage * (dayIntensity - nightIntensity);
        float newCurrentIntensity=0;
        if (sunSetting)
        {
            newCurrentIntensity = currentSunIntensity - intensityOverTime;
            if(newCurrentIntensity < nightIntensity)
            {
                newCurrentIntensity = nightIntensity - newCurrentIntensity;
                newCurrentIntensity=nightIntensity + Mathf.Abs(newCurrentIntensity);
                sunSetting = false;
            }
        }
        else if (!sunSetting)
        {
            newCurrentIntensity = currentSunIntensity + intensityOverTime*1.2f;
            if (newCurrentIntensity > dayIntensity)
            {
                newCurrentIntensity = dayIntensity + newCurrentIntensity;
                newCurrentIntensity = dayIntensity - Mathf.Abs(newCurrentIntensity);
                sunSetting = true;
            }
        }

        currentSunIntensity = newCurrentIntensity;

        UpdateSun(currentSunIntensity);
    }

    void SetTime(float time)
    {
        //time = Mathf.Abs(Mathf.Sin((time) *.5f * Mathf.PI));
        float sunIntensity= nightIntensity + time*(dayIntensity - nightIntensity);
        
        UpdateSun(sunIntensity);
    }

    public void UpdateSun(float intensity)
    {
        Debug.Log("sun update: "+currentSunIntensity);

        targetSunIntensity = Mathf.Clamp(intensity, nightIntensity, dayIntensity);
        if(changingTime==null)
            changingTime=StartCoroutine(GraduallyChangeSunIntensity(1f));
        
    }

    IEnumerator GraduallyChangeSunIntensity(float speed)
    {
        while(sun.intensity!=targetSunIntensity)
        {
            sun.intensity = Mathf.MoveTowards(sun.intensity,targetSunIntensity,Time.deltaTime *speed);
            yield return null;
        }
        changingTime = null;
    }

    void UpdateLights(float sunIntensityRatio)
    {
        foreach (var activeLight in activeLights)
        {
            activeLight.UpdateLight(sunIntensityRatio);
        }
    }

    float GetSunIntensityRatio(float sunIntensity)
    {
        return sunIntensity / (dayIntensity - nightIntensity);
    }
}
