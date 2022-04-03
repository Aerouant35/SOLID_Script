using System.Linq;
using UnityEngine;

public class AttackZeppelinCommand : GameCommand
{
    private readonly AnimationComponent m_playerAnimation;
    private AttackZeppelinComponent m_atkZeppelinCom;
    private string m_sPlayerName;
    
    public AttackZeppelinCommand(string playerName, Transform player)
    {
        m_playerAnimation = player.GetComponent<AnimationComponent>();
        m_atkZeppelinCom = player.GetComponent<AttackZeppelinComponent>();
        
        m_sPlayerName = playerName;
    }
    
    public override void Execute()
    {
        if (ReferenceEquals(m_playerAnimation, null)) return;
        
        m_playerAnimation.SetTrigger();
        m_atkZeppelinCom.sPlayerName = m_sPlayerName;
    }
}
