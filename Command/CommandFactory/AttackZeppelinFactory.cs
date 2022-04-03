using System.Linq;
using UnityEngine;

public class AttackZeppelinFactory
{
    public static GameCommand GetAttackZeppelinCommand(string playerName, Transform player)
    {
        var attackComponentTimer = player.GetComponent<AttackZeppelinComponent>().GetTimer();
        if (ReferenceEquals(attackComponentTimer, null)) return null;
        
        return attackComponentTimer.IsCooldownDone() ? new AttackZeppelinCommand(playerName, player) : null;    
    }
}
