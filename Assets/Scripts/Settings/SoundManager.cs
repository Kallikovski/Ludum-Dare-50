using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer Mixer;
    public Slider Slider;
    public PreferencesManager Preferences;

    private void Start()
    {
        Preferences.LoadPrefs();
        Slider.value = Preferences.volume;
        SetLevel(Slider.value);
    }
    public void SetLevel(float sliderValue)
    {
        Mixer.SetFloat("AudioVolume", Mathf.Log10(sliderValue) * 20);
    }
    private void OnDestroy()
    {
        Preferences.volume = Slider.value;
        Preferences.SavePrefs();
    }
}
