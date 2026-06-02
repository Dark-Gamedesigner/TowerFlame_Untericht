using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave")]
public class Wave : ScriptableObject
{
    public List<WaveBatch> Batches;
    public int Reward;
    private float _currentWave;

    public GameObject GetTopMostEnemy(){
        return Batches[0].EnemyType;
    }

    public float GetTopMostDelay(){
        return Batches[0].delayPerEnemy;
    }

    public void Pop(){
        WaveBatch waveBatch = Batches[0];       // Wir holen uns die höchste Batch als Kopie
        waveBatch.Amount -= 1;                  // Senken den Amount (Die übrigen zu spawnenden Gegner) um 1
        if (waveBatch.Amount <= 0){             // Sollte der Batch leer sein:
            Batches.RemoveAt(0);           // Batch löschen
        }
        else{
            Batches[0] = waveBatch; // Kopie ersetzt die erste Batch
        }
        else if (_currentWave <= 0){
            
        }
    }
}
