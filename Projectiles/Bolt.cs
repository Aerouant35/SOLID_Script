using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : Projectile
{
    #region RuntimeVariables
    //protected GameManager m_gameManager;
    //[SerializeField] private Rigidbody m_rigidBody;
    //[SerializeField] private float m_fShootForce = 3000f;
    //[SerializeField] private float m_fLifeTime = 5f;
    private bool m_bHasDammageCastle = false;
    private int m_nbDamage;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        m_gameManager = GameManager.instance;
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.AddForce(transform.forward * m_fShootForce);
        Destroy(gameObject, m_fLifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        //do once
        if(!m_bHasDammageCastle)
        {
            m_bHasDammageCastle = true;
            m_gameManager.TakeDamage(m_nbDamage);
        }
    }
    
    public void SetDamage(int nbDamage)
    {
        m_nbDamage = (int)nbDamage;
    }

    override protected void OnHit(Collision other)
    {
        base.OnHit(other);

        //AOE


        //Destroy(gameObject);
    }
}
