using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour{
    public List<GameObject> SpawnableEnemy; //Todo mehrere Prefabs
    
    private float _nextSpawnTime;

    public EnemyPathControll FirstNode;

    public List<Wave> Wave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        _nextSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_nextSpawnTime <= Time.time){           // Wenn der nächste Spawn-Zeitpunkt vergangen ist:
            _nextSpawnTime = Time.time + Wave[0].Batches[0].delayPerEnemy;
            
            var newSpawn = Instantiate(Wave[0].Batches[0].EnemyType, transform.position, Quaternion.identity);
            newSpawn.GetComponent<Enemy>().Target = FirstNode;
        }
    }

    private int _randomEnemyIndex(){
        int selectedEnemyIndex = Random.Range(0, SpawnableEnemy.Count);
        return selectedEnemyIndex;
    }
}
