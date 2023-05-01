using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class musiccontroller : MonoBehaviour
{
    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
        FindObjectOfType<Slider>().value = savedVolume;
        ChangeVolume(savedVolume);
    }

    public AudioMixerGroup Mixer;

    public void ChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0f, volume));
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

}