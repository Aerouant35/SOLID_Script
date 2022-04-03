using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    [SerializeField] private string m_TagToCompare;
    
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(m_TagToCompare)) return;
        
 //       Debug.Log("Hit: " + other.gameObject.name);
            
        other.GetComponent<AEnemy>()?.Death();

        Destroy(gameObject);
    }
}
