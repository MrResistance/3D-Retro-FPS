using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixerGroup amg;
    private string thisAMGname;
    // Start is called before the first frame update
    void Start()
    {
        thisAMGname = amg.name.ToString() + " Volume";
        if (!PlayerPrefs.HasKey(thisAMGname))
        {
            PlayerPrefs.SetFloat(thisAMGname, Mathf.Log(1) * 20);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        amg.audioMixer.SetFloat(thisAMGname, Mathf.Log(volumeSlider.value) * 20);
        Save();
    }
    private void Save()
    {
        PlayerPrefs.SetFloat(thisAMGname, volumeSlider.value);
    }
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(thisAMGname);
    }

    public void MuteVolume()
    {
        if (volumeSlider.value == volumeSlider.minValue)
        {
            amg.audioMixer.SetFloat(thisAMGname, Mathf.Log(1) * 20);
            volumeSlider.value = volumeSlider.maxValue;
        }
        else if (volumeSlider.value > volumeSlider.minValue)
        {
            amg.audioMixer.SetFloat(thisAMGname, Mathf.Log(0) * 20);
            volumeSlider.value = volumeSlider.minValue;
        }
        Save();
    }
}
