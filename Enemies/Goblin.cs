using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Goblin : AEnemy
{
    #region Variables
    [SerializeField] private SoGameEvent m_GoblinMove;
    [SerializeField] private SoGameEvent m_GoblinPunch;
    [SerializeField] private SoGameEvent m_GoblinDie;
    #endregion


    // Update is called once per frame
    override public void Update()
    {
        MoveGoblin();
        AttackGoblin();       
    }

    void MoveGoblin()
    {
        if (Vector3.Distance(transform.position, targetPosition) >= 0.5f && !animation.GetBool("IsDead"))
        {
            Move();
            m_GoblinMove.Raise();
        }
        else if(currentState != States.attacking && !m_bStopMoving ){
            animation.SetBool("IsAttacking", true);
            m_bStopMoving = true;
            currentState = States.attacking;
        }  
    }

    void AttackGoblin()
    {
        if (currentState == States.attacking && TimeLeft <= 0)
        {
//            Punch();
            m_GoblinPunch.Raise();
            animation.SetBool("IsIdle", false);
            TimeLeft = m_AtkCooldown;
        }
        else
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft <= 0)
            {
                animation.SetBool("IsIdle", true);
            }
        }
    }

    public void Punch()
    {
        _GM.TakeDamage(GetDamage());
    }

    public override void Death()
    {
        //do once
        if( !m_bIsDead)
        {
            //play sfx
            m_GoblinDie.Raise();

            //play anim
            animation.SetBool("IsDead", true);
            animation.SetTrigger("TriggDie");
            m_bIsDead = true;

            SpawnManager.instance.m_GoblinEnemyList.Remove(gameObject.transform);
        }
    }

    public void EndDeathAnim()
    {
        Destroy(this.gameObject, 0f);
    }
}
