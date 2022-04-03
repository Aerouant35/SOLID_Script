using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private ParticleSystem[] m_arrPartciles;
    [SerializeField] private float m_fLifeTime = 3f;
    #endregion Variables

    public void Play()
    {
        foreach(ParticleSystem currentParticle in m_arrPartciles)
        {
            currentParticle.Play();
        }
        
        Destroy(gameObject, m_fLifeTime);
    }
}
