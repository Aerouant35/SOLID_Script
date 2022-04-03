using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawnManager : MonoBehaviour
{
    #region Variables
    public static PlayerSpawnManager instance;
    
    //LocalGame
    public Transform currentPlayer = null;

    [SerializeField] private TwitchChat m_TwitchChat;
    
    [SerializeField] private Transform m_PlayerPrefab;
    [SerializeField] private List<Transform> m_PlayerSpawner;
    
    [SerializeField] private List<Transform> m_PlayerSpawnerRemaining;

    // for new twitch player incoming
    public Dictionary<string, Transform> playerDic = new Dictionary<string, Transform>();
    public Dictionary<string, Transform> playerSpawnerDic = new Dictionary<string, Transform>();
    #endregion

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
        }
    }
    
    private void Start()
    {
        ResetSpawner();
        
        if (m_TwitchChat == null)
        {
            m_TwitchChat = TwitchChat.instance;
        }

        if (m_TwitchChat != null)
        {
            for (int i = 0; i < m_TwitchChat.players.Count; i++)
            {
                PlayerJoin(m_TwitchChat.players[i]);
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("VirtualPlayer1"))
        {
            var virtualPlayer = VirtualPlayerFactory.GetVirtualPlayerCommand("VirtualPlayer1");
            if (ReferenceEquals(virtualPlayer, null)) return;

            GameCommandExecutor.instance.AddCommand(virtualPlayer);
        }
        
        if (Input.GetButtonDown("VirtualPlayer2"))
        {
            var virtualPlayer = VirtualPlayerFactory.GetVirtualPlayerCommand("VirtualPlayer2");
            if (ReferenceEquals(virtualPlayer, null)) return;

            GameCommandExecutor.instance.AddCommand(virtualPlayer);
        }

        if (Input.GetButtonDown("VirtualPlayer3"))
        {
            var virtualPlayer = VirtualPlayerFactory.GetVirtualPlayerCommand("VirtualPlayer3");
            if (ReferenceEquals(virtualPlayer, null)) return;

            GameCommandExecutor.instance.AddCommand(virtualPlayer);
        }
    }
    
    /// <summary>
    /// New player join the game
    /// Create player at random position in wall
    /// </summary>
    public void PlayerJoin(string playerName)
    {
        var nbRand = Random.Range(0, m_PlayerSpawnerRemaining.Count);
        var playerSpawner = m_PlayerSpawnerRemaining[nbRand];
        
        if (playerSpawner != null)
        {
            playerDic.Add(playerName, Instantiate(m_PlayerPrefab, playerSpawner.position, playerSpawner.rotation));
            playerSpawnerDic.Add(playerName, m_PlayerSpawnerRemaining[nbRand]);
        }
        
        m_PlayerSpawnerRemaining.Remove(playerSpawner);
    }

    /// <summary>
    /// Player leave the game
    /// Destroy the player and set player spawner available
    /// </summary>
    private void PlayerDisconnect(string playerName)
    {
        Destroy(playerDic[playerName].gameObject);
        playerDic.Remove(playerName);
        
        m_PlayerSpawnerRemaining.Add(playerSpawnerDic[playerName]);
        playerSpawnerDic.Remove(playerName);
    }

    /// <summary>
    /// Initialize player after load saveFile
    /// </summary>
    /// <param name="playerName">Player name</param>
    /// <param name="position">Player spawner</param>
    public void SetPlayer(string playerName, Vector3 position)
    {
        var playerSpawner = m_PlayerSpawnerRemaining.Find(spawnerTransform => spawnerTransform.position == position);
        if (ReferenceEquals(playerSpawner, null))
        {
            Debug.LogError("Can't find Spawner in m_PlayerSpawnerRemaining");
            return;
        }

        playerDic.Add(playerName, Instantiate(m_PlayerPrefab, playerSpawner.position, playerSpawner.rotation));
        playerSpawnerDic.Add(playerName, playerSpawner);

        m_PlayerSpawnerRemaining.Remove(playerSpawner);
    }
    
    /// <summary>
    /// Reset dictionaries and Spawn list  
    /// </summary>
    public void ResetSpawner()
    {
        if (playerDic.Count > 0)
        {
            foreach (var player in playerDic)
            {
                Destroy(player.Value.gameObject);
            }
            
            playerDic.Clear();
        }
        
        if (playerSpawnerDic.Count > 0)
            playerSpawnerDic.Clear();

        if (m_PlayerSpawnerRemaining.Count > 0)
            m_PlayerSpawnerRemaining.Clear();
        
        m_PlayerSpawnerRemaining = new List<Transform>(m_PlayerSpawner);
    }
}
