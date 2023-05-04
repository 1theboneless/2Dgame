using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class mastercontroller : MonoBehaviour
{
   

    public AudioMixerGroup Mixer;

    public void ChangeVolume(float VolumeMaster)
    {
        Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0f, VolumeMaster));
        PlayerPrefs.SetFloat("MasterVolume", VolumeMaster);
    }
    
}