using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer mixer;

    public const string MUSIC_KEY = "MusicVolume";
    public const string SFX_KEY = "SfxVolume";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadVolume();
    }

    void LoadVolume()
    {
        float MusicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float SfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(Volume.MIXER_MUSIC, Mathf.Log10(MusicVolume) * 20);
        mixer.SetFloat(Volume.MIXER_SFX, Mathf.Log10(SfxVolume) * 20);
    }
}