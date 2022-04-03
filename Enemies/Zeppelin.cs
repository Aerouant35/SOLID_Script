using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeppelin : AEnemy
{
    #region Variables
    [SerializeField] private ParticlesManager m_particlesManager;
    private bool m_bIsDying = false;
    [SerializeField] private GameObject[] m_goVfxSmokes;
    [SerializeField] private SoGameEvent m_destroyEvent;
    #endregion Variables


    // Update is called once per frame
    public override void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) >= 0.5f)
        {
            Move();
        }
        else if(!m_bIsDying)
        {
            OnTargetHit();
        }
    }

    void OnTargetHit()
    {
        //do once
        m_bIsDying = true;

        //deal dmg to castle
        _GM.TakeDamage(GetDamage());
        
        Death();
    }
    
    public override void Death()
    {
        //do Once
        if( !m_bIsDead )
        {
            //remove reference AerialEnemyList
            SpawnManager.instance.m_AerialEnemyList.Remove(gameObject.transform);

            //play SFX
            m_destroyEvent.Raise();
            //play VFX
            if (m_particlesManager)
            {
                m_particlesManager.Play();
            }

            //hide 3D Model - TODO : Fadding ( pb : Transparency Z-order, need to change Mat from Opaque to Transparent at runtime)
            //gameObject.GetComponentInChildren<Renderer>().enabled = false;
            gameObject.GetComponentInChildren<Renderer>().gameObject.SetActive(false);

            //disable Smoke FX
            foreach (GameObject currentVfx in m_goVfxSmokes)
            {
                currentVfx.SetActive(false);
            }

            m_bIsDead = true;

            //destroy
            Destroy(gameObject, 1f);
        }
    }
}
