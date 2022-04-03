using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum States
{
    moving,
    attacking
};

public abstract class AEnemy : MonoBehaviour
{
    #region SerializedVariables
    [SerializeField] private SOHealth m_soHealth;
    [SerializeField] private SODamage m_soDamage;    
    [SerializeField] private Transform target;
    public Vector3 targetPosition;
    [SerializeField] private float speed = 5;
    public Animator animation;
    #endregion SerializedVariables

    #region RuntimeVariables    
    protected GameManager _GM;
    public States currentState;
    protected bool m_bIsDead = false;
    protected bool m_bStopMoving = false;
    public float m_AtkCooldown = 2;
    public float TimeLeft;
    #endregion RuntimeVariables

    // Start is called before the first frame update
    void Start()
    {
        _GM = GameManager.instance;
        currentState = States.moving;
    }

    public virtual void Update()
    {
        Move();
    }

    //Move AI to target position
    public void Move()
    {
        Vector3 dir = targetPosition - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

	//Set a new target
    public void setTarget(Vector3 newTargetPosition){
        targetPosition = newTargetPosition;
    }

	//return damage from scriptable object
    public int GetDamage()
    {
        return m_soDamage.GetNbDamage();
    }

    public abstract void Death();
}

