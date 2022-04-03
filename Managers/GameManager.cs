using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    #region Variables
    [SerializeField] private SOHealth m_soHealth;
    public float StartingHealth = 1000;
    public static GameManager instance;
    public bool bIsDead = false;
    #endregion Variables
    

    
    private void Awake()
    {
        if (instance != null)
            DestroyImmediate(this);
        else
            instance = this;
    }
    void Start()
    {
        m_soHealth.SetHealth((int)StartingHealth);
    }

    public void TakeDamage(int i)
    {
        if(m_soHealth.GetHealth() > 0)
        {
            m_soHealth.TakeDamage(i);
        }

        if (m_soHealth.GetHealth() <= 0)
        {
            //do once
            if(!bIsDead)
            {
                bIsDead = true;
                UiManager.inst.Lose();
            }
        }
    }

	public float GetCastleHealth(){
		return (float)m_soHealth.GetHealth()/StartingHealth;
	}
}
