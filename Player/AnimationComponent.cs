using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    [SerializeField] private Animator m_PlayerAnimator;
    [SerializeField] private Animator m_BowAnimator;
    [SerializeField] private string m_TriggerName;

    public void SetTrigger()
    {
        m_PlayerAnimator.SetTrigger(m_TriggerName);
        m_BowAnimator.SetTrigger(m_TriggerName);
    }
}
