using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MuteMusic : MonoBehaviour
{
    public AudioMixerGroup Mixer;
    public AudioMixerSnapshot normal;
    public AudioMixerSnapshot menu;

    private void OnEnable()
    {
        menu.TransitionTo(0.5f);
    }
    private void OnDisable()
    {
        normal.TransitionTo(0.5f);
    }
}
