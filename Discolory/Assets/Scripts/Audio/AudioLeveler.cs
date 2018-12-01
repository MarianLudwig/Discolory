using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioLeveler : MonoBehaviour {

    public AudioMixer mainMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        GameData.Instance.LoadSettings();
        musicSlider.value = GameData.Instance.musicVolume;
        sfxSlider.value = GameData.Instance.sfxVolume;
    }

    // Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
    public void SetMusicLevel(float musicLevel)
    {
        if(musicLevel <= -60)
            mainMixer.SetFloat("musicVol", -80);
        else
        mainMixer.SetFloat("musicVol", musicLevel);
        GameData.Instance.musicVolume = musicLevel;
    }

    // Call this function and pass in the float parameter sfxLevel to set the volume of the AudioMixerGroup SoundFx in mainMixer
    public void SetSfxLevel(float sfxLevel)
    {
        if (sfxLevel <= -60)
            mainMixer.SetFloat("sfxVol", -80);
        else
            mainMixer.SetFloat("sfxVol", sfxLevel);
        GameData.Instance.sfxVolume = sfxLevel;
    }

}
