using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer Mixer;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider EffectsSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        EffectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 1);
        ChangeVolumes();
    }
    public void ChangeVolumes()
    {

        Mixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0f, MasterSlider.value));
        PlayerPrefs.SetFloat("MasterVolume", MasterSlider.value);
        Mixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0f, MusicSlider.value));
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        Mixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0f, EffectsSlider.value));
        PlayerPrefs.SetFloat("EffectsVolume", EffectsSlider.value);
    }
}