using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    private bool m_bIsSelected = false;
    [SerializeField] private GameObject m_goSelected;

    // Update is called once per frame
    void Update()
    {
        if (PlayerSpawnManager.instance.currentPlayer == this.transform)
        {
            if (Input.GetButtonDown("ShootArrow"))
            {
                var currentPlayerName = PlayerSpawnManager.instance.playerDic
                    .FirstOrDefault(player => player.Value == PlayerSpawnManager.instance.currentPlayer).Key;
                var zeppelinAttack = AttackZeppelinFactory.GetAttackZeppelinCommand(currentPlayerName, transform);
                if (ReferenceEquals(zeppelinAttack, null)) return;
                
                GameCommandExecutor.instance.AddCommand(zeppelinAttack);
            }
        
            if (Input.GetButtonDown("ShootCatapult"))
            {
                var currentPlayerName = PlayerSpawnManager.instance.playerDic
                    .FirstOrDefault(player => player.Value == PlayerSpawnManager.instance.currentPlayer).Key;
                var ballistaAttack = AttackBallistaFactory.GetAttackBallistaCommand(currentPlayerName, transform);
                if (ReferenceEquals(ballistaAttack, null)) return;
                
                GameCommandExecutor.instance.AddCommand(ballistaAttack);
            }
        
            if (Input.GetButtonDown("ShootMachicolation "))
            {
                var currentPlayerName = PlayerSpawnManager.instance.playerDic
                    .FirstOrDefault(player => player.Value == PlayerSpawnManager.instance.currentPlayer).Key;
                var goblinAttack = AttackGoblinFactory.GetAttackGoblinCommand(currentPlayerName, transform);
                if (ReferenceEquals(goblinAttack, null)) return;
                
                GameCommandExecutor.instance.AddCommand(goblinAttack);
            }
        }
    }

    public void SetActive(bool bIsActive)
    {
        m_bIsSelected = bIsActive;
        m_goSelected.SetActive(m_bIsSelected);
    }
}
