using UnityEngine;

public class AttackBallistaCommand : GameCommand
{
    private AttackBallistaComponent m_ballistaComponent;
    private string m_sPlayerName;
    
    public AttackBallistaCommand(string playerName, Transform player)
    {
        m_ballistaComponent = player.GetComponent<AttackBallistaComponent>();
        m_sPlayerName = playerName;
    }
    
    public override void Execute()
    {
        //if there is at least one ballista, rotate the spawner to shot toward it
        if( SpawnManager.instance.m_BallistaEnemyList.Count > 0 )
        {
            Debug.Log("Catapult rotation");

            //target the first ballista of the list
            Transform trCurrentTarget = SpawnManager.instance.m_BallistaEnemyList[0];

            //Spawner Rotation
            //            Vector3 vec3Rotation = (m_ballistaComponent.trSpawnRock.position - trCurrentTarget.position).normalized;
            //
            //            vec3Rotation.x = trCurrentTarget.rotation.x;
            //            vec3Rotation.y = trCurrentTarget.rotation.y;
            //
            //            trCurrentTarget.rotation = vec3Rotation;
            //
            //            m_ballistaComponent.trSpawnRock.LookAt(trCurrentTarget, m_ballistaComponent.trSpawnRock.up);
            //            m_ballistaComponent.trSpawnRock.LookAt(new Vector3(trCurrentTarget.position.x, m_ballistaComponent.trSpawnRock.position.y, trCurrentTarget.position.z));
            //            m_ballistaComponent.trSpawnRock.LookAt(new Vector3(m_ballistaComponent.trSpawnRock.position.x, trCurrentTarget.position.y, m_ballistaComponent.trSpawnRock.position.z));

            //Spawner Translation
            Vector3 vec3NewPosition = m_ballistaComponent.trSpawnRock.position;
            vec3NewPosition.x = trCurrentTarget.transform.position.x;

            m_ballistaComponent.trSpawnRock.position = vec3NewPosition;  
        }
      
        //Default shoot
        GameObject currentRock = Object.Instantiate(m_ballistaComponent.goRockPrefab, m_ballistaComponent.trSpawnRock.position, m_ballistaComponent.trSpawnRock.rotation).gameObject;    
        currentRock.GetComponentInChildren<ProjectilePseudo>().SetPseudo(m_sPlayerName);
    }
}
