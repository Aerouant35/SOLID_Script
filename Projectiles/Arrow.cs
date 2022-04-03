using System;
using UnityEngine;

namespace Projectiles
{
    public class Arrow: Projectile
    {
        #region Variables
        [SerializeField] private string m_sTagToCompare = "Enemy";
        [SerializeField] private string m_sTMethodToCall = "Death";
        [SerializeField] float m_fMoveSpeed = 10f;
    
        [HideInInspector] public Transform target;

        private Vector3 m_tDirection;
        #endregion

        private void FixedUpdate()
        {
            if (target != null)
            {
                transform.LookAt(target);
                
                m_tDirection = (transform.position - target.transform.position).normalized;
                m_rigidBody.MovePosition(transform.position - m_tDirection * m_fMoveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                m_rigidBody.MovePosition(transform.position - (transform.forward.x < 0 ? -transform.forward : transform.forward) * m_fMoveSpeed * Time.fixedDeltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(m_sTagToCompare)) return;
            
            other.gameObject.SendMessage(m_sTMethodToCall);
            
            //Play SFX
            if(m_soOnHitEvent != null)
            {
                m_soOnHitEvent.Raise();
            }

            //Play VFX
            if (m_goOnHitParticle != null)
            {
                //instanciate particle at object position
                Instantiate(m_goOnHitParticle, gameObject.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
