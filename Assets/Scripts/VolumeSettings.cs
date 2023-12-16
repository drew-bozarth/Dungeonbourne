using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private Slider sfxSlider;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        musicSource.volume = volume;
    }
    
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        sfxSource.volume = volume;
    }
}
