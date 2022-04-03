using System.Linq;
using Projectiles;
using UnityEngine;

public class AttackZeppelinComponent : AttackComponent
{
    #region SerializedFields
    //prefabs
    [SerializeField] private GameObject m_goArrowPrefab;
    
    //spawn positions
    [SerializeField] private Transform m_trSpawnArrow;

    [HideInInspector] public string sPlayerName;
    #endregion

    public void SpawnArrow()
    {
        var vPos = Vector3.zero;
        var target = SpawnManager.instance.m_AerialEnemyList.FirstOrDefault();
        
        //Zeppelin in visible on screen
        if (Camera.main != null && target != null)
            vPos = Camera.main.WorldToViewportPoint(target.position);

        var arrow = Instantiate(m_goArrowPrefab, m_trSpawnArrow.position, m_trSpawnArrow.rotation);
        arrow.GetComponent<Arrow>().target = target != null && vPos.x <= 1f ? target : null;
        arrow.GetComponentInChildren<ProjectilePseudo>().SetPseudo(sPlayerName);
    }
}
