using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region RuntimeVariables
    protected GameManager m_gameManager;
    [SerializeField] protected Rigidbody m_rigidBody;
    [SerializeField] protected float m_fShootForce = 2000f;
    [SerializeField] protected float m_fLifeTime = 5f;
    [SerializeField] protected GameObject m_goOnHitParticle;
    [SerializeField] protected SoGameEvent m_soOnHitEvent;
    [SerializeField] protected SoGameEvent m_soOnLaunchEvent;
    #endregion

    // Start is called before the first frame update
    public virtual void Start()
    {
        //Init var
        m_gameManager = GameManager.instance;
        m_rigidBody = GetComponent<Rigidbody>();
        //Launch Projectible
        m_rigidBody.AddForce(transform.forward * m_fShootForce);
        //Define lifetime
        Destroy(gameObject, m_fLifeTime);
        //Play SFX
        if(m_soOnLaunchEvent != null)
        {
            m_soOnLaunchEvent.Raise();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        OnHit(other);
    }

    protected virtual void OnHit(Collision other)
    {
        //Play SFX
        if(m_soOnHitEvent != null)
        {
            m_soOnHitEvent.Raise();
        }

        //Play VFX
        if (m_goOnHitParticle != null)
        {
            //instanciate particle at object position
            var particleManager = Instantiate(m_goOnHitParticle, gameObject.transform.position, Quaternion.identity);
            particleManager.GetComponent<ParticlesManager>().Play();
        }
    }
}
