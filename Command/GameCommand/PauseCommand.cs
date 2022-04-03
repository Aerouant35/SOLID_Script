using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCommand : GameCommand
{
    private GameObject m_goPause;
    
    public PauseCommand(GameObject goPause)
    {
        m_goPause = goPause;
    }
    
    public override void Execute()
    {
        m_goPause.SetActive(true);
        Time.timeScale = 0f;
    }
}
