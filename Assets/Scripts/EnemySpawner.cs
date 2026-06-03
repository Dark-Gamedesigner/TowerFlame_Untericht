using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class EnemySpawner : MonoBehaviour{
    private float _nextSpawnTime;
    public EnemyPathControll FirstNode;
    public List<Wave> Wave;
    private bool _waveIsRunning = false;
    public int CurrentWave{ get; private set; } = 0;
    public static UnityEvent<int> Wavestarted = new ();
    public static UnityEvent<int> WaveFinisched = new();
    
    // TODO ADD WaveCounter
    // TODO Set _waveIsRunning false, sobald die Wave zu ende ist
    // TODO Nach der ersten Wave wird dann die zweite erscheinen, sobald wir noch mal "StartWave " aufrufen (Knopf)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        for (int i = 0; i < Wave.Count; i++){
            Wave[i] = Instantiate(Wave[i]);     // Wir erstellen Kopien der SOs, um nicht das original zu überschreiben
        }
    }

    [ContextMenu("Start Wave")]     // Erlaubt es dem Unity Editor heraus diese Funktion aufzurufen.
    public void StartWave(){
        _waveIsRunning = true;
        _nextSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_waveIsRunning){           // Wenn der nächste Spawn-Zeitpunkt vergangen ist:
           /* _nextSpawnTime = Time.time + Wave[0].Batches[0].delayPerEnemy;
            
            var newSpawn = Instantiate(Wave[0].Batches[0].EnemyType, transform.position, Quaternion.identity);
            newSpawn.GetComponent<Enemy>().Target = FirstNode;*/
           if (_nextSpawnTime < Time.time && !Wave[0].IsEmpty()){
               var newestEnemy = Instantiate(Wave[0].GetTopMostEnemy(), transform.position, Quaternion.identity);
               _nextSpawnTime += Wave[0].GetTopMostDelay();
               Wave[0].Pop();
               newestEnemy.GetComponent<Enemy>().Target = FirstNode;
           }
           else{
               if (Wave[0].IsEmpty()){
                   var anyEnemy = GameObject.FindAnyObjectByType<Enemy>();
                   if (anyEnemy == null){
                       _waveIsRunning = false;
                       Bank.AddMoney.Invoke(Wave[0].Reward);
                       Wave.RemoveAt(0);
                       WaveFinisched.Invoke(CurrentWave);
                   }
               }
           }
        }
    }
}
    /*private int _randomEnemyIndex(){
        int selectedEnemyIndex = Random.Range(0, Wave.Count);
        return selectedEnemyIndex;
    }*/

