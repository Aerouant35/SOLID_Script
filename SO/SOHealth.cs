using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Health", menuName = "ScriptableObjects/Health", order = 1)]
public class SOHealth : ScriptableObject
{
    #region Variables
    public int m_nbHealth = 100;
    #endregion Variables

    public void TakeDamage(int nbDamage)
    {
        m_nbHealth -= nbDamage;
    }

    public void SetHealth(int nbHealthToAdd)
    {
        m_nbHealth = nbHealthToAdd;
    }

    public int GetHealth()
    {
        return m_nbHealth;
    }
}
