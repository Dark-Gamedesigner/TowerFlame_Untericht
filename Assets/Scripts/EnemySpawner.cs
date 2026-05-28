using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour{
    public List<GameObject> SpawnableEnemy; //Todo mehrere Prefabs
    
    private float _nextSpawnTime;

    public EnemyPathControll FirstNode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        _nextSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_nextSpawnTime <= Time.time){           // Wenn der nächste Spawn-Zeitpunkt vergangen ist:
            _nextSpawnTime = Time.time + 2f;
            
            var newSpawn = Instantiate(SpawnableEnemy[_randomEnemyIndex()], transform.position, Quaternion.identity);
            newSpawn.GetComponent<Enemy>().Target = FirstNode;
        }
    }

    private int _randomEnemyIndex(){
        int selectedEnemyIndex = Random.Range(0, SpawnableEnemy.Count);
        return selectedEnemyIndex;
    }
}
