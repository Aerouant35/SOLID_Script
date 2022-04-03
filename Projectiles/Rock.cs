using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Projectile
{
    [SerializeField] private float m_fRadiusAOE = 3f;

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override protected void OnHit(Collision other)
    {
        base.OnHit(other);

        //AOE
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, m_fRadiusAOE);
        foreach (var hitCollider in hitColliders)
        {
            if( hitCollider.tag == "Enemy")
            {
                hitCollider.SendMessage("Death");
            }
        }

        //Play VFX

        //Play SFX

        Destroy(gameObject);
    }
}
