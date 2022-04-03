using UnityEngine;

public static class AttackGoblinFactory
{
    public static GameCommand GetAttackGoblinCommand(string playerName, Transform player)
    {
        var attackComponentTimer = player.GetComponent<AttackGoblinComponent>().GetTimer();
        if (ReferenceEquals(attackComponentTimer, null)) return null;
        return attackComponentTimer.IsCooldownDone() ? new AttackGoblinCommand(playerName, player) : null;    
    }
}
