using UnityEngine;

public class AttackBallistaFactory
{
    public static GameCommand GetAttackBallistaCommand(string playerName, Transform player)
    {
        var attackComponentTimer = player.GetComponent<AttackBallistaComponent>().GetTimer();
        if (ReferenceEquals(attackComponentTimer, null)) return null;
        
        return attackComponentTimer.IsCooldownDone() ? new AttackBallistaCommand(playerName, player) : null;    
    }
}
