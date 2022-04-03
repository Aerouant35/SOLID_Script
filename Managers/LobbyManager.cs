using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private TwitchChat m_TwitchChat;
    public Text PlayerText;
    public void Play()
    {
        LevelManager.inst.PlayAnimation();
    }
    
    void Update () {
        if (m_TwitchChat == null)
        {
            m_TwitchChat = TwitchChat.instance;
        }

        string message = "";
        for (int i = 0; i < m_TwitchChat.players.Count; i++)
        {
            string displayedName = UppercaseFirst(m_TwitchChat.players[i]);
            message += displayedName+"    ";
        }
        PlayerText.text = message;
    }
    
    static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}
