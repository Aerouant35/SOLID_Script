using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public enum spawnState {nextWave, Wave, end};

public class SpawnManager : MonoBehaviour
{
    #region Variables
	public static SpawnManager instance;
    [SerializeField] private SOWave m_soWaveManager;

    [SerializeField] private SOSpawnedEnemy m_soGoblin;
    [SerializeField] private SOSpawnedEnemy m_soZeppelin;
    [SerializeField] private SOSpawnedEnemy m_soBallista;

    //List of target Point in the scene
    [SerializeField] private Transform m_SpawnPoint;
    [SerializeField] private Transform m_GoblinTargetPoint;
    [SerializeField] private List<Transform> m_ZeppelinTargetPoint;
    [SerializeField] private Transform m_BallistaTargetPoint;

    [SerializeField] private spawnState m_currState;
	
    //List of all enemies
	public List<Transform> m_GoblinEnemyList;
	public List<Transform> m_AerialEnemyList;
    public List<Transform> m_BallistaEnemyList;
    //Chances
    private int m_nbTotalSpawnChances;
    #endregion Variables

    private void Awake()
    {
        if (instance != null)
            DestroyImmediate(this);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
	    m_currState = spawnState.nextWave;
	    m_soWaveManager.SetTimeLeft(0);
	    m_soWaveManager._nbEnemyLeft = m_soWaveManager._nbEnemy ;
	    m_soWaveManager._currentWave = 1;
	    m_nbTotalSpawnChances = m_soGoblin.m_SpawnChance + m_soZeppelin.m_SpawnChance + m_soBallista.m_SpawnChance;
    }

    // Update is called once per frame
    void Update()
    {
	    if (m_currState == spawnState.nextWave)
	    {
		    if (m_soWaveManager.GetTimeLeft() <= 0)
		    {
			    m_currState = spawnState.Wave;
		    }
		    else
		    {
			    m_soWaveManager.ReduceTimeLeft(Time.deltaTime);
		    }
	    }

	    if (m_currState == spawnState.Wave)
	    {
		    if (m_soWaveManager._currentWave <= m_soWaveManager._nbWave)
		    {
			    if(m_soWaveManager._nbEnemyLeft>0){
				    if (m_soWaveManager.GetTimeLeft() <= 0)
				    {
					    SpawnEnemy();
					    m_soWaveManager.SetTimeLeft(m_soWaveManager.GetSpawnRate());
				    }
				    else
				    {
					    m_soWaveManager.ReduceTimeLeft(Time.deltaTime);
				    }
			    }

		    }
		    CheckWaveStates();
	    }
    }

    public spawnState getStates()
    {
	    return m_currState;
    }
    

    void CheckWaveStates()
    {
	    if (m_BallistaEnemyList.Count + m_AerialEnemyList.Count + m_GoblinEnemyList.Count <= 0 &&
	        m_soWaveManager._nbEnemyLeft <= 0)
	    {
		    if (m_soWaveManager._currentWave < m_soWaveManager._nbWave)
		    {
			    m_soWaveManager._currentWave++;
			    m_soWaveManager._nbEnemyLeft = m_soWaveManager._nbEnemy ;
			    m_soWaveManager.SetTimeLeft(m_soWaveManager._timeBetweenWaves);
			    m_currState = spawnState.nextWave;
		    }
		    else
		    {
			    UiManager.inst.Win();
		    }

	    }
    }
	//Instantiate enemy
    void SpawnEnemy()
    {
        int RandomEnemy =  Random.Range(0, m_nbTotalSpawnChances);

        switch (RandomEnemy)
        {
            case int n when (n <= m_soGoblin.m_SpawnChance):
            {
                //Instantiate a goblin
                var randomOffset = new Vector3(Random.Range(-5f, 5f), 0, 0);
                Transform go = Instantiate(m_soGoblin.m_Prefab, m_SpawnPoint.position + randomOffset, m_SpawnPoint.rotation);
                go.GetComponent<AEnemy>().setTarget(m_GoblinTargetPoint.position + randomOffset);
                m_GoblinEnemyList.Add(go);//Instantiate enemyPrefab
                break;
            }
            case int n when (n > m_soGoblin.m_SpawnChance && n < m_soGoblin.m_SpawnChance + m_soZeppelin.m_SpawnChance):
            {
                //Instantiate a Zeppelin
                var randomOffset = new Vector3(Random.Range(-5f, 5f), 10, 23);
                Transform go = Instantiate(m_soZeppelin.m_Prefab, m_SpawnPoint.position + randomOffset, m_SpawnPoint.rotation);
                var randomTargetPoint = Random.Range(0, 2);
                switch (randomTargetPoint)
                {
	                case 0:
		                go.GetComponent<AEnemy>().setTarget(m_ZeppelinTargetPoint[0].position);
		                break;
		            case 1:
			            go.GetComponent<AEnemy>().setTarget(m_ZeppelinTargetPoint[1].position);
			            break;
			        default:
				        go.GetComponent<AEnemy>().setTarget(m_ZeppelinTargetPoint[0].position);
				        break;
                }
                m_AerialEnemyList.Add(go);//Instantiate enemyPrefab
                break;
            }
            case int n when (n > m_soGoblin.m_SpawnChance + m_soZeppelin.m_SpawnChance && n < m_soGoblin.m_SpawnChance + m_soZeppelin.m_SpawnChance + m_soBallista.m_SpawnChance):
            {
                //Instantiate a Ballista
                var randomOffset = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-1f, 1f));
                Transform go = Instantiate(m_soBallista.m_Prefab, m_SpawnPoint.position + randomOffset, m_SpawnPoint.rotation);
                go.GetComponent<AEnemy>().setTarget(m_BallistaTargetPoint.position + randomOffset);
                m_BallistaEnemyList.Add(go);//Instantiate enemyPrefab
                break;
            }
        }
        m_soWaveManager._nbEnemyLeft--;  
    }
}
