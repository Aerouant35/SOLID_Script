using UnityEngine;

public class AttackGoblinComponent : AttackComponent
{
    #region SerializedFields
    //prefabs
    public GameObject goRockPrefab;
    
    //spawn positions
    [HideInInspector] public Transform trSpawnRock;

    [SerializeField] private string m_sObjectToFind;
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        trSpawnRock = GameObject.Find(m_sObjectToFind).GetComponent<Transform>();
    }
}
