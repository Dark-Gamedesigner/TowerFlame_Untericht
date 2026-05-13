using System;
using UnityEngine;

public class EnemyPathControll : MonoBehaviour{

    public EnemyPathControll next;
    public bool isEnd;

    private void OnDrawGizmos(){
        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(transform.position, 1);
        if (next!=null){
            Gizmos.DrawLine(transform.position, next.transform.position);
        }
    }
}
