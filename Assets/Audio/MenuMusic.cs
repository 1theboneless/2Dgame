using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MenuMusic : MonoBehaviour
{
    public AudioMixerGroup Mixer;
    public AudioMixerSnapshot normal;
    public AudioMixerSnapshot pause;

    private void OnEnable()
    {
        pause.TransitionTo(0);
    }
    private void OnDisable()
    {
        normal.TransitionTo(0);
    }
}
