using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    #region Variables
    private byte[] m_bResult;
    private string m_sFilePath;
    
    [SerializeField] private Throbbing m_tThrobber;

    [Header("File properties")]
    [SerializeField] private string m_sSavePath;
    [SerializeField] private string m_sSaveFileName;
    [SerializeField] private string m_sSaveFileExtension;

    [Header("Scriptable Object")]
    [SerializeField] private SOHealth m_SOHealth;
    [SerializeField] private SOWave m_SOWave;
    #endregion
    
    private void Start()
    {
        var directoryPath = Path.Combine(Application.persistentDataPath, m_sSavePath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        
        m_sFilePath = Path.Combine(directoryPath, m_sSaveFileName + m_sSaveFileExtension);
    }
    
    public void SaveData()
    {
        m_tThrobber.gameObject.SetActive(true);
        StartCoroutine(GetDataCoroutine(async data => await WriteData(data)));
    }

    public async void LoadData()
    {
        m_tThrobber.gameObject.SetActive(true);
        var data = await ReadData();
        SetData(data);
    }

    private IEnumerator GetDataCoroutine(Action<SaveData> callback)
    {
        if (ReferenceEquals(m_SOHealth, null))
        {
            Debug.LogError("m_SOHealth can't be null");
            yield break;
        }

        if (ReferenceEquals(m_SOWave, null))
        {
            Debug.LogError("m_SOHealth can't be null");
            yield break;
        }

        var playersData = new List<Player>();
        
        foreach (var playerSpawner in PlayerSpawnManager.instance.playerSpawnerDic)
        {
            var player = new Player(playerSpawner.Key, playerSpawner.Value);

            playersData.Add(player);

            yield return null;
        }

        var waveData = new Wave
        {
            currentWave = m_SOWave._currentWave,
            nbEnemy = m_SOWave._nbEnemy,
            nbEnemyLeft = m_SOWave._nbEnemyLeft,
            nbWave = m_SOWave._nbWave,
            timeBetweenWaves = m_SOWave._timeBetweenWaves
        };
        
        var data = new SaveData
        {
            castleHealth = m_SOHealth.GetHealth(),
            wave = waveData,
            players = playersData
        };
        
        callback.Invoke(data);
    }

    private async Task WriteData(SaveData data)
    {
        if (string.IsNullOrEmpty(m_sFilePath))
        {
            Debug.LogError("m_sFilPath can't be empty");
            return;
        }

        var bytes = await Task.Run(() => Encoding.UTF8.GetBytes(JsonUtility.ToJson(data, true)));
        
        using (var fileStream = new FileStream(m_sFilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
        {
            await fileStream.WriteAsync(bytes, 0, bytes.Length);
        }
        m_tThrobber.gameObject.SetActive(false);
    }
    
    private async Task<SaveData> ReadData()
    {
        using (var fileStream = new FileStream(m_sFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            m_bResult = new byte[fileStream.Length];
            await fileStream.ReadAsync(m_bResult, 0, (int)fileStream.Length);
        }

        return await Task.Run(() => JsonUtility.FromJson<SaveData>(Encoding.UTF8.GetString(m_bResult)));
    }

    private void SetData(SaveData data)
    {
        if (ReferenceEquals(m_SOHealth, null))
        {
            Debug.LogError("m_SOHealth can't be null");
            return;
        }

        if (ReferenceEquals(m_SOWave, null))
        {
            Debug.LogError("m_SOHealth can't be null");
            return;
        }

        m_SOHealth.m_nbHealth = data.castleHealth;
        
        m_SOWave._currentWave = data.wave.currentWave;
        m_SOWave._nbEnemy = data.wave.nbEnemy;
        m_SOWave._nbEnemyLeft = data.wave.nbEnemyLeft;
        m_SOWave._nbWave = data.wave.nbWave;
        m_SOWave._timeBetweenWaves = data.wave.timeBetweenWaves;

        PlayerSpawnManager.instance.ResetSpawner();

        foreach (var dataPlayer in data.players)
        {
            PlayerSpawnManager.instance.SetPlayer(dataPlayer.playerName, dataPlayer.position);
        }
        
        m_tThrobber.gameObject.SetActive(false);
    }
}
