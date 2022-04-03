using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cooldown", menuName = "ScriptableObjects/Cooldown")]
public class SOCooldown : ScriptableObject
{
    public float fCooldown = 10;

    public bool IsCooldownDone(float runtimeCooldown)
    {
        return runtimeCooldown > fCooldown;
    }    
}
