using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region SerializedVariables
    //Mixer
    [SerializeField] private AudioMixer m_audioMixer;
    //Sliders
    [SerializeField] private Slider m_sliderMasterVolume;
    [SerializeField] private Slider m_sliderMusicVolume;
    [SerializeField] private Slider m_sliderGameVolume;
    //Mixers group names
    [SerializeField] private string m_strMasterVolume = "MasterVolume";
    [SerializeField] private string m_strMusicVolume = "MusicVolume";
    [SerializeField] private string m_strGameVolume = "GameVolume";
    //curve
    [SerializeField] private AnimationCurve _audioCurve;
    #endregion


    void Start()
    {
        //Assign Saved Value to slider - Master
        if(PlayerPrefs.HasKey(m_strMasterVolume))
        {
            float valueMasterVolume = PlayerPrefs.GetFloat(m_strMasterVolume);
            m_sliderMasterVolume.SetValueWithoutNotify(valueMasterVolume);
            ChangeAudioMixer(valueMasterVolume, m_strMasterVolume);
        }
        //Assign Saved Value to slider - Music
        if (PlayerPrefs.HasKey(m_strMusicVolume))
        {
            float valueMusicVolume = PlayerPrefs.GetFloat(m_strMusicVolume);
            m_sliderMusicVolume.SetValueWithoutNotify(valueMusicVolume);
            ChangeAudioMixer(valueMusicVolume, m_strMusicVolume);
        }
        //Assign Saved Value to slider - Game
        if (PlayerPrefs.HasKey(m_strGameVolume))
        {
            float valueGameVolume = PlayerPrefs.GetFloat(m_strGameVolume);
            m_sliderGameVolume.SetValueWithoutNotify(valueGameVolume);
            ChangeAudioMixer(valueGameVolume, m_strGameVolume);
        }
    }

    #region VolumeChange
    public void ChangeVolumeMain(float value)
    {
        VolumeChanged( value, m_strMasterVolume);
    }
    public void ChangeVolumeMusic(float value)
    {
        VolumeChanged( value, m_strMusicVolume);
    }

    public void ChangeVolumeGame(float value)
    {
        VolumeChanged( value, m_strGameVolume);
    }
    #endregion


    #region Utility
    private void VolumeChanged(float value, string strMixer)
    {
        ChangeAudioMixer(value, strMixer);
        PlayerPrefs.SetFloat(strMixer, value);
        PlayerPrefs.Save();
    }

    void ChangeAudioMixer(float value, string strMixer)
    {
        float dbValue = _audioCurve.Evaluate(value);
        m_audioMixer.SetFloat(strMixer, dbValue);
    }
    #endregion
}
