using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    #region Variables
    public static UiManager inst;
    private GameManager _GM;
    [SerializeField] private SpawnManager m_spawnManagerInstance;
    
    [SerializeField] private SOWave m_soWave;
    //Spawn UI
    [SerializeField] private TMP_Text m_NextWaveText;
    [SerializeField] private TMP_Text m_currentWaveText;
    [SerializeField] private TMP_Text m_nbEnnemyLeftText;
    
    [SerializeField] private Slider m_slider;
    
    [SerializeField] private SoGameEvent m_soGameEventWin;
    [SerializeField] private SoGameEvent m_soGameEventLose;

    public GameObject goWin;
    public GameObject goLoose;
    public GameObject goPause;
    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        _GM = GameManager.instance;
        m_spawnManagerInstance = SpawnManager.instance;
        
        if (goPause.activeSelf) goPause.SetActive(false);

        //m_canvas.renderMode = RenderMode.ScreenSpaceCamera;
        //m_canvas.worldCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_spawnManagerInstance == null)
        {
            m_spawnManagerInstance = SpawnManager.instance;
        }
        m_slider.value = _GM.GetCastleHealth();
        if (m_spawnManagerInstance != null)
        {
            m_nbEnnemyLeftText.text = "Enemies Left : " + (m_soWave._nbEnemyLeft + m_spawnManagerInstance.m_BallistaEnemyList.Count +
                                                           m_spawnManagerInstance.m_AerialEnemyList.Count + m_spawnManagerInstance.m_GoblinEnemyList.Count);
            m_currentWaveText.text = "Current Wave : " + (m_soWave._currentWave);
            if (m_spawnManagerInstance.getStates() == spawnState.nextWave)
            {
                m_NextWaveText.enabled = true;
                m_NextWaveText.text = "Next Wave in :" + System.Math.Round(m_soWave.GetTimeLeft(),0);
            }
            else
            {
                m_NextWaveText.enabled = false;
            }
        }

        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
    }

    public void Win()
    {
        goWin.SetActive(true);
        m_soGameEventWin.Raise();
    }

    public void Lose()
    {
        goLoose.SetActive(true);
        Time.timeScale = 0.1f;
        m_soGameEventLose.Raise();
    }

    private void Pause()
    {
        var pauseCommand = PauseFactory.GetPauseCommand();
        
        if (ReferenceEquals(pauseCommand, null)) return;
                
        GameCommandExecutor.instance.AddCommand(pauseCommand);
    }

    public void Resume()
    {
        goPause.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void ReturnMenu()
    {
        LevelManager.inst.PlayAnimation();
    }
}
