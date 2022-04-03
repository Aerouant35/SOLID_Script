using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SliderValueDisplay : MonoBehaviour {
    public Slider sliderUI;
    public TMP_Text textSliderValue;
    [SerializeField] private SOWave m_soWave; 

    void Start (){
        ShowSliderValue();
    }

    public void ShowSliderValue () {
        textSliderValue.text = sliderUI.value.ToString();
    }

    public void ChangeWavesSettings()
    {
        m_soWave._nbWave = (int)sliderUI.value;
    }

    public void ChangeEnemySettings()
    {
        m_soWave._nbEnemy = (int)sliderUI.value;
    }

    public void ChangeWaveTimer()
    {
        m_soWave._timeBetweenWaves = sliderUI.value;
    }
}