using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class musiccontrollermenu : MonoBehaviour
{
    public static musiccontrollermenu instance; // ��������� ����������� ���� ��� ������� � ������� �� ������ �������� � ����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // ����������� ������ �� ���� ������, ���� ����� �� ���� ��������� ������ �����������
            DontDestroyOnLoad(gameObject); // �� ���������� ���� ������ ��� �������� �� ������ �����
        }
        else
        {
            Destroy(gameObject); // ���������� ���� ������, ���� ����� ��� ��� �������� ������ ���������
        }
    }

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
        ChangeVolume(savedVolume);
    }

    public AudioMixerGroup Mixer;

    public void ChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0f, volume));
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

}