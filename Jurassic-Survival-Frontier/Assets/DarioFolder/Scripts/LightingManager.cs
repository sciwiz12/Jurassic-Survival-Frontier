using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    [Header("Variables")]
    [SerializeField] public float timerDay = 1;
    [SerializeField, Range(0, 24)] private float timeOfDay;

    #region PROPERTIES

    private float TimeOfDay {  get { return timeOfDay; } set { timeOfDay = value; } }

    #endregion

    public bool isNight;

    private void Update()
    {
        TimeOfDay += Time.deltaTime * timerDay;
        TimeOfDay %= 24;
        
        if(Preset == null) return;
        if(Application.isPlaying)
        {
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170, 0));
        }

        if (timeOfDay % 24 >= 5 && timeOfDay % 24 <= 18)
        {
            isNight = false;
        }
        else
        {
            isNight=true;
        }
    }

    private void OnValidate()
    {
        if(DirectionalLight != null)
        {
            return;
        }

        if(RenderSettings.sun != null)
        {
            DirectionalLight=RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight=light;
                    return;
                }
            }
        }
    }
}