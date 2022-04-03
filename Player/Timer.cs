using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private SOCooldown m_AttackCooldowns;

    private float m_RuntimeCooldown;
    private float m_NextCooldown;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        m_NextCooldown = m_AttackCooldowns.fCooldown;
        m_RuntimeCooldown = 0;
    }

    private void Update()
    {
            m_RuntimeCooldown = Time.time;
    }

    public bool IsCooldownDone()
    {
        var result = m_RuntimeCooldown > m_NextCooldown;

        if (result)
        {
            m_NextCooldown = m_AttackCooldowns.fCooldown + Time.time;
        }
        
        return result;
    }

    #region Utility
    public void SetSoCooldown(SOCooldown sOCooldown)
    {
        m_AttackCooldowns = sOCooldown;
        Init();
    }
    #endregion Utility
}
