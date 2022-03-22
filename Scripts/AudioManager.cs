using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM;
    public Slider BGMSlider;

    void Start() {
        LoadValues();
    }

    public void ChangeBGM(AudioClip Music) {
        BGM.Stop();
        BGM.clip = Music;
        BGM.Play();
    }

    public void SaveVolumeButtom() {
        float volumeValue = BGMSlider.value;
        PlayerPrefs.SetFloat("BGMVolume", volumeValue);     //Saves slider float values
        LoadValues();
    }

    void LoadValues() {
        float volumeValue = PlayerPrefs.GetFloat("BGMVolume");      //loads in slider float values
        BGMSlider.value = volumeValue;
        BGM.volume = volumeValue;
    }
}
