using UnityEngine;

public class RockMachicolation : Projectile
{
    [SerializeField] private float m_fRadiusAOE = 2f;
    [SerializeField] private string m_sTagToCompare = "Enemy";
    [SerializeField] private string m_sTMethodToCall = "Death";
    
    protected override void OnHit(Collision other)
    {
        base.OnHit(other);
        
        //AOE
        var hitColliders = Physics.OverlapSphere(gameObject.transform.position, m_fRadiusAOE);
        
        foreach (var hitCollider in hitColliders)
        {
            if (!hitCollider.CompareTag(m_sTagToCompare)) continue;
            
            hitCollider.SendMessage(m_sTMethodToCall);
            Destroy(gameObject);
        }
    }
}
