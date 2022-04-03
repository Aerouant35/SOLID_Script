using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 1)]
public class SOWave : ScriptableObject
{
    #region Variables

    public int _currentWave;
    public int _nbEnemy;
    public int _nbEnemyLeft;
    public int _nbWave;
    public float _timeBetweenWaves;
    public int _maxPlayer;
    [SerializeField] private float m_SpawnRate;
    [SerializeField] private float m_TimeLeft;
    #endregion Variables

    void Start()
    {
        _currentWave = 1;
    }
    public float GetSpawnRate()
    {
        return m_SpawnRate;
    }
    
    public float GetTimeLeft()
    {
        return m_TimeLeft;
    }

    public void SetTimeLeft(float i)
    {
        m_TimeLeft = i;
    }
    public void ReduceTimeLeft(float i)
    {
        m_TimeLeft -= i;
    }
}
