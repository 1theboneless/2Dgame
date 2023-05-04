using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class musiccontroller : MonoBehaviour
{
 

    public AudioMixerGroup Mixer;

    public void ChangeVolume(float VolumeMusics)
    {
        Mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0f, VolumeMusics));
        PlayerPrefs.SetFloat("MusicVolume", VolumeMusics);
    }
    
}