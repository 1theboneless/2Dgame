using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class effectscontroller : MonoBehaviour
{
   

    public AudioMixerGroup Mixer;

    public void ChangeVolume(float VolumeEffects)
    {
        Mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0f, VolumeEffects));
        PlayerPrefs.SetFloat("EffectsVolume", VolumeEffects);
    }
    
}