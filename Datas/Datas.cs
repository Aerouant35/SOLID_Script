using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SaveData
{
    public int castleHealth;
    public Wave wave;   
    public List<Player> players;
}

[System.Serializable]
public struct Player
{
    public string playerName;
    public Vector3 position;
    
    public Player(string playerName, Transform spawner)
    {
        this.playerName = playerName;
        position = spawner.position;
    }
}

[System.Serializable]
public struct Wave
{
    public int currentWave;
    public int nbWave;
    public int nbEnemy;
    public int nbEnemyLeft;
    public float timeBetweenWaves;
}