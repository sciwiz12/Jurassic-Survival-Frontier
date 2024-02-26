using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    #region FIELDS

    [SerializeField] private AudioData sceneBackgroundMusic = null;
    [SerializeField] private AudioSource musicSource = null;
    [SerializeField] private AudioSource effectsSource = null;
    [SerializeField] private float fadeDuration = 1f;

    #endregion

    public override void Awake()
    {
        base.Awake();
        sceneBackgroundMusic.Play(musicSource);
        musicSource.playOnAwake = false;
        musicSource.volume = sceneBackgroundMusic.Volume;
        musicSource.Play();
        
    }

    #region METHODS

    public void PlayEffect(AudioData effect) {
        effect.PlayOneShot(effectsSource);
    }

    public void ChangeMusicTrack(AudioData newMusic, bool instantly = false)
    {
        if (instantly)
        {
            newMusic.Play(musicSource);
            return;
        }
    }

    #endregion
}
