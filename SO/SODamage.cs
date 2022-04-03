using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Damage", menuName = "ScriptableObjects/Damage", order = 1)]
public class SODamage : ScriptableObject
{
    #region Variables
    [SerializeField] private int m_nbDamage = 100;
    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetNbDamage()
    {
        return m_nbDamage;
    }
    
}
