﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 using System.Net.Sockets;
using System.IO;

 public enum TwitchStates
 {
     Menu,
     Join,
     InGame,
     Connexion
 };
public class TwitchChat : MonoBehaviour {

    #region Variables

    private TwitchStates m_CurrentState;
    public static TwitchChat instance;

    [SerializeField] private GoogleSheet m_google;
    
    [SerializeField] private SpawnManager m_SpawnManager;
    [SerializeField] private PlayerSpawnManager m_PlayerSpawn;

    [SerializeField] private SOTwitchChat m_soTwitchChat;
    [SerializeField] private SOWave m_soWave;
    
    public List<String> players;

    #endregion
    
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        _ = m_google.CleanGoogleSheetTask();
    }

    
    void Update () {
        if (m_SpawnManager == null)
        {
            m_SpawnManager = SpawnManager.instance;
        }
        if (m_PlayerSpawn == null)
        {
            m_PlayerSpawn = PlayerSpawnManager.instance;
        }
        if(m_CurrentState == TwitchStates.Menu || m_CurrentState == TwitchStates.Connexion)
        {
            if(players.Count > 0)
            {
                players.Clear();
            }
        }

        if (m_CurrentState == TwitchStates.Join || m_CurrentState == TwitchStates.InGame)
        {
            if (!m_soTwitchChat.twitchClient.Connected)
            {
                Connect();
            }
            if (m_soTwitchChat.twitchClient.Connected)
            {
                ReadChat();
            }
        }
    }

    /// <summary>
    /// Connexion to twitch
    /// </summary>
    public void Connect()
    {
        m_soTwitchChat.twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        m_soTwitchChat.reader = new StreamReader(m_soTwitchChat.twitchClient.GetStream());
        m_soTwitchChat.writer = new StreamWriter(m_soTwitchChat.twitchClient.GetStream());

        m_soTwitchChat.writer.WriteLine("PASS " + m_soTwitchChat.password);
        m_soTwitchChat.writer.WriteLine("NICK " + m_soTwitchChat.username);
        m_soTwitchChat.writer.WriteLine("USER " + m_soTwitchChat.username + " 8 * :" + m_soTwitchChat.username);
        m_soTwitchChat.writer.WriteLine("JOIN #" + m_soTwitchChat.channelName);
        m_soTwitchChat.writer.Flush();
    }

    /// <summary>
    /// Read chat entry 
    /// </summary>
    private void ReadChat()
    {
        if(m_soTwitchChat.twitchClient.Available > 0)
        {
            var message = m_soTwitchChat.reader.ReadLine(); //Read in the current message
            print(message);
            if (message.Contains("!"))
            {
                //Get the users name by splitting it from the string
                var splitPoint = message.IndexOf("!", 1);
                var chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                //Get the users message by splitting it from the string
                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);

                if (message.StartsWith("!"))
                {
                    //Run the instructions to control the game!
                    GameInputs(message, chatName);
                    print(message);
                    _ = m_google.GoogleSheetTask();
                }
            }
        }
    }

    /// <summary>
    /// Twitch input command
    /// </summary>
    /// <param name="ChatInputs"></param>
    /// <param name="Name"></param>
    private void GameInputs(string ChatInputs, string Name)
    {

        if (m_CurrentState == TwitchStates.Join)
        {
            if (ChatInputs.ToLower() == "!join")
            {
                if (!players.Contains(Name) && m_soWave._maxPlayer > players.Count)
                {
                    players.Add(Name);
                }
            }
        }

        if (m_CurrentState != TwitchStates.InGame) return;
        if (!players.Contains(Name)) return;
        if (players.Count <= 0) return;
        switch (ChatInputs.ToLower())
        {

            case "!g":
            case "!goblin":
                var goblinAttack = AttackGoblinFactory.GetAttackGoblinCommand(Name, m_PlayerSpawn.playerDic[Name]);
                if (ReferenceEquals(goblinAttack, null)) break;

                GameCommandExecutor.instance.AddCommand(goblinAttack);
                break;
            case "!z":
            case "!zeppelin":
                var zeppelinAttack = AttackZeppelinFactory.GetAttackZeppelinCommand(Name, m_PlayerSpawn.playerDic[Name]);
                if (ReferenceEquals(zeppelinAttack, null)) break;
                
                GameCommandExecutor.instance.AddCommand(zeppelinAttack);
                break;
            case "!b":
            case "!ballista":
                var ballistaAttack = AttackBallistaFactory.GetAttackBallistaCommand(Name, m_PlayerSpawn.playerDic[Name]);
                if (ReferenceEquals(ballistaAttack, null)) break;
                
                GameCommandExecutor.instance.AddCommand(ballistaAttack);
                break;
        }
    }
    
    public void changeState(TwitchStates newState)
    {
        m_CurrentState = newState;
    }

    public TwitchStates getState()
    {
        return m_CurrentState;
    }
}