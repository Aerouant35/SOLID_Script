using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ballista : AEnemy
{
    #region Variables
    [SerializeField] private GameObject m_goBoltPrefab;
    [SerializeField] private GameObject m_goBoltSpawnPosition;
    [SerializeField] private SoGameEvent m_shootEvent;
    #endregion

    override public void Update()
    {
        MoveBallista();
        Attack();
    }

    void MoveBallista()
    {
        if (Vector3.Distance(transform.position, targetPosition) >= 0.5f && !m_bIsDead)
        {
            animation.SetBool("bIsMoving", true);
            Move();
        }
        else if (currentState != States.attacking && !m_bStopMoving)
        {
            animation.SetBool("bIsMoving", false);
            m_bStopMoving = true;
            currentState = States.attacking;
        }
    }

    void Attack()
    {
        if (currentState == States.attacking && TimeLeft <= 0)
        {
            animation.SetTrigger("Fire");
            animation.SetBool("bIsMoving", false);
            TimeLeft = m_AtkCooldown;
        }
        else if ( currentState == States.attacking)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft <= 0)
            {
                animation.SetBool("bIsMoving", true);
            }
        }
    }

    public void Shoot()
    {
        m_shootEvent.Raise();
        GameObject goCurrentBolt = Instantiate(m_goBoltPrefab, m_goBoltSpawnPosition.transform.position, m_goBoltSpawnPosition.transform.rotation);
        goCurrentBolt.GetComponent<Bolt>().SetDamage(GetDamage());
    }

    public override void Death()
    {
        SpawnManager.instance.m_BallistaEnemyList.Remove(gameObject.transform);
        //animation.SetBool("IsDead", true);
        m_bIsDead = true;
        Destroy(this.gameObject);
    }
}
