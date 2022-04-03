using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Variables
    public static LevelManager inst;
    [SerializeField] private Animator m_animator;
    [SerializeField] private float m_fTransitionWaitingTime = 1f;
    [SerializeField] private TwitchChat m_TwichChat;

    enum enumCurrentScene { Menu, Connexion, Lobby, Game};
    private enumCurrentScene m_enumCurrentScene = enumCurrentScene.Menu;
    private bool m_bIsPlayingAnimation = false;
    #endregion Variables

    private void Awake()
    {
        if (inst != null && inst != this)
            Destroy(this.gameObject);
        else
        {
            inst = this;
            DontDestroyOnLoad(inst.gameObject);
        }
    }


    public void PlayAnimation()
    {
        //disable slow Motion
        Time.timeScale = 1;
        //do once
        if(!m_bIsPlayingAnimation)
        {
            m_bIsPlayingAnimation = true;
            //Play animation
            m_animator.SetTrigger("FadeIn");         
        }
    }


    public void FadeInEnd()
    {
        //Debug.Log("Anim Event - FadeIn : end");

        //Load next level
        switch(m_enumCurrentScene)
        {
            case enumCurrentScene.Menu:
            {
                LoadConnexion();
                break;
            }
            case enumCurrentScene.Lobby:
            {
                LoadGame();
                break;
            }
            case enumCurrentScene.Game:
            {
                LoadMenu();
                break;
            }
            case enumCurrentScene.Connexion:
            {
                LoadLobby();
                break;
            }
        }

        m_animator.SetTrigger("FadeOut");
        m_bIsPlayingAnimation = false;
    }

    #region SceneLoading
    private void LoadMenu()
    {
        m_enumCurrentScene = enumCurrentScene.Menu;
        SceneManager.LoadScene("Assets/Scenes/Menu.unity", LoadSceneMode.Single);
        if (m_TwichChat == null)
        {
            m_TwichChat = TwitchChat.instance;
        }
        //m_TwichChat.InGameState(); //TODO : disable twitch input 
    }
    
    private void LoadConnexion()
    {
        m_enumCurrentScene = enumCurrentScene.Connexion;
        SceneManager.LoadScene("Assets/Scenes/Connexion.unity", LoadSceneMode.Single);
        SceneManager.LoadScene("Assets/Scenes/Twitch.unity", LoadSceneMode.Additive);
        if (m_TwichChat == null)
        {
            m_TwichChat = TwitchChat.instance;
        }
        if (m_TwichChat != null)
        {
            m_TwichChat.changeState(TwitchStates.Connexion);
        }
    }
    private void LoadLobby()
    {
        m_enumCurrentScene = enumCurrentScene.Lobby;
        SceneManager.LoadScene("Assets/Scenes/Lobby.unity", LoadSceneMode.Single);
        //SceneManager.LoadScene("Assets/Scenes/Twitch.unity", LoadSceneMode.Additive);
        if (m_TwichChat == null)
        {
            m_TwichChat = TwitchChat.instance;
        }
        if (m_TwichChat != null)
        {
            m_TwichChat.changeState(TwitchStates.Join);
        }

    }



    private void LoadGame()
    {
        m_enumCurrentScene = enumCurrentScene.Game;
        SceneManager.LoadScene("Assets/Scenes/Start.unity", LoadSceneMode.Single);
        SceneManager.LoadScene("Assets/Scenes/Interface.unity", LoadSceneMode.Additive);
        SceneManager.LoadScene("Assets/Scenes/Graph.unity", LoadSceneMode.Additive);
        SceneManager.LoadScene("Assets/Scenes/Spawn.unity", LoadSceneMode.Additive);
        SceneManager.LoadScene("Assets/Scenes/Audio.unity", LoadSceneMode.Additive);
        if (m_TwichChat == null)
        {
            m_TwichChat = TwitchChat.instance;
        }
        m_TwichChat.changeState(TwitchStates.InGame);
//        SceneManager.LoadScene("Assets/Scenes/Twitch.unity", LoadSceneMode.Additive);
    }
    #endregion SceneLoading
}
