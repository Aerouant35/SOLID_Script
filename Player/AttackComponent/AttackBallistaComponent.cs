using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBallistaComponent : AttackComponent
{
    #region Variables
    //prefabs
    public GameObject goRockPrefab;
    
    //spawn positions
    [HideInInspector] public Transform trSpawnRock;
    #endregion

    public void Start()
    {
        base.Start();
        trSpawnRock = GameObject.Find("SpawnRock").GetComponent<Transform>();
    }
}
