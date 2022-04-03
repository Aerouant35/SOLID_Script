using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    #region SerilizedVariables
    [SerializeField] private SOCooldown m_soTimerCooldown;
    #endregion

    #region RuntimeVariables
    protected Timer m_timer;
    #endregion

    #region Accessor
    public Timer GetTimer() { return m_timer; }
    #endregion


    protected virtual void Start()
    {
        m_timer = gameObject.AddComponent<Timer>() as Timer;
        m_timer.SetSoCooldown(m_soTimerCooldown);
    }
}
