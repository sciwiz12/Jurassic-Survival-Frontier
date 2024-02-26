using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Data", menuName = "Sounds/Audio Data")]
public class AudioData : ScriptableObject
{
    #region FIELDS

    [SerializeField] private AudioClip clip;

    public AudioClip Clip 
    { 
        get => clip;
        set => clip = value;
    }

    [Range(0, 256)][SerializeField] private int priority = 128;
    
    public int Priority => priority;

    [Range(0,1)] [SerializeField] private float volume;
    
    public float Volume => volume;

    [Range(0, 1)][SerializeField] private float pitch;
    
    public float Pitch => pitch;
    
    [Range(0, 1)][SerializeField] private float stereoPan;
    
    public float StereoPan => stereoPan;
    
    [Range(0, 1)][SerializeField] private float spatialBlend;
    
    public float SpatialBlend => spatialBlend;
    
    [Range(0, 1)][SerializeField] private float reverbZoneMix;
    public float ReverbZone => reverbZoneMix;

    #endregion

    public void Play(AudioSource source)
    {
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.priority = priority;
        source.panStereo = stereoPan;
        source.spatialBlend = spatialBlend;
        source.reverbZoneMix = reverbZoneMix;
        source.Play();
    }

    public void PlayOneShot(AudioSource source)
    {
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.priority = priority;
        source.panStereo = stereoPan;
        source.spatialBlend = spatialBlend;
        source.reverbZoneMix = reverbZoneMix;
        source.PlayOneShot(clip);
    }
}
