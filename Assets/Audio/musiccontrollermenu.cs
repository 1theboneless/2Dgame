using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class musiccontrollermenu : MonoBehaviour
{
    public static musiccontrollermenu instance; // Публичное статическое поле для доступа к скрипту из других объектов и сцен

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Присваиваем ссылку на этот объект, если ранее не было назначено других экземпляров
            DontDestroyOnLoad(gameObject); // Не уничтожаем этот объект при переходе на другую сцену
        }
        else
        {
            Destroy(gameObject); // Уничтожаем этот объект, если ранее уже был назначен другой экземпляр
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