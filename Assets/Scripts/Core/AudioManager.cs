using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public const string MASTERVOLUME = "MasterVolumeParamameter";
    public const string SFXVOLUME = "SFXVolumeParameter";
    public const string BGMVOLUME = "BGMVolumeParameter";
    public float masterVolume = 1.0f;
    public float bgmVolume = 1.0f;
    public float sfxVolume = 1.0f;

    public AudioMixer audioMixer;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Attepmted to create a second audio manager");
            Destroy(this);
        }
    }

    private float ConvertToDecibel(float value)
    {
        float newVolume = value;
        if (newVolume <= 0)
        {
            // If we are at zero, set our volime to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are above zero, so start by finding the log10 value
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20db range (instead od 1-0 db)
            newVolume = newVolume * 20;
        }

        return newVolume;
    }

    public void OnMasterVolumeChange(float value)
    {
        masterVolume = Mathf.Clamp01(value);
        float newVolume = ConvertToDecibel(value);

        // Set the volume to the new volume setting
        audioMixer.SetFloat(MASTERVOLUME, newVolume);
    }
    public void OnSFXVolumeChange(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        float newVolume = ConvertToDecibel(value);

        // Set the volume to the new volume setting
        audioMixer.SetFloat(SFXVOLUME, newVolume);
    }
    public void OnBGMVolumeChange(float value)
    {
        bgmVolume = Mathf.Clamp01(value);
        float newVolume = ConvertToDecibel(value);

        // Set the volume to the new volume setting
        audioMixer.SetFloat(BGMVOLUME, newVolume);
    }
}
