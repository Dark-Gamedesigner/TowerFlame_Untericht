using System;
using UnityEngine;

public class TowerSocket : MonoBehaviour
{
    public GameObject HeldTower;

    private void OnDrawGizmos(){
        Color myColor = HeldTower ? Color.blueViolet : Color.aquamarine;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.8f);
    }
}
