using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameSettingsUI : MonoBehaviour
{

    [SerializeField] private SOWave m_waveManager;
    [SerializeField] private Slider m_nbEnemy;
    [SerializeField] private Slider m_nbWave;
    [SerializeField] private Slider m_timeBetweenWave;

    void Start()
    {
        m_nbEnemy.value = m_waveManager._nbEnemy;
        m_nbWave.value = m_waveManager._nbWave;
        m_timeBetweenWave.value = m_waveManager._timeBetweenWaves;
        
    }
}
