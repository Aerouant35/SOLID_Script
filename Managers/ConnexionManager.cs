using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ConnexionManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private TwitchChat m_twitchInst;
    [SerializeField] private SOTwitchChat m_TwitchChat;

    [SerializeField] private TMP_Text ErrorText;
    [SerializeField] private TMP_InputField name;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField channelName;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ErrorText.enabled = false;
        name.text = m_TwitchChat.username;
        password.text = m_TwitchChat.password;
        channelName.text = m_TwitchChat.channelName;
    }
    
    public void Play()
    {
        if (m_twitchInst == null)
        {
            m_twitchInst = TwitchChat.instance;
        }
        m_TwitchChat.username = name.text.ToLower();
        m_TwitchChat.password = password.text;
        m_TwitchChat.channelName = channelName.text.ToLower();
        
        if (m_twitchInst != null)
        {
            m_twitchInst.Connect();
            string message = m_TwitchChat.reader.ReadLine();
            Debug.Log(message);
            if (message.Contains("NOTICE"))
            {
                ErrorText.enabled = true;
            }
            else
            {
                LevelManager.inst.PlayAnimation();
            }
        }
    }
}
