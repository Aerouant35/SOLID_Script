using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AttackGoblinCommand : GameCommand
{
    private AttackGoblinComponent m_goblinComponent;
    private string m_sPlayerName;

    public AttackGoblinCommand(string playerName, Transform player)
    {
        m_goblinComponent = player.GetComponent<AttackGoblinComponent>();
        m_sPlayerName = playerName;
    }
    
    public override void Execute()
    {
        var goblinCompPos = m_goblinComponent.trSpawnRock.position;
        var target = SpawnManager.instance.m_GoblinEnemyList.FirstOrDefault(goblin => goblin.position.z <= 7f);
        
        var spawnPosition = new Vector3(target != null ? target.position.x : goblinCompPos.x,
            goblinCompPos.y, goblinCompPos.z);
        
        var rock = Object.Instantiate(m_goblinComponent.goRockPrefab, spawnPosition, Quaternion.identity).gameObject;
        rock.GetComponentInChildren<ProjectilePseudo>().SetPseudo(m_sPlayerName);
    }
}
