using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;

[CreateAssetMenu(fileName = "Twitch", menuName = "ScriptableObjects/Twitch", order = 1)]
public class SOTwitchChat : ScriptableObject
{
    #region Variables

    public TcpClient twitchClient;
    public StreamReader reader;
    public StreamWriter writer;
    
    public string username, password, channelName; //Get the password from https://twitchapps.com/tmi

    #endregion
}
