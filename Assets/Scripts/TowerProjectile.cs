using System;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    public float Speed = 5;

    public float RemainingLifeTime;
    public int Strength = 2;
    public DemageType demageType = DemageType.Kinematic;
 
    // Update is called once per frame
    void FixedUpdate(){
        LifeTimeHandling();

        float distanceThisFrame = Speed * Time.deltaTime;
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distanceThisFrame + Single.Epsilon, LayerMask.GetMask("Enemies"));
        if (hasHit){
            var enemyComponent = hit.collider.GetComponent<Enemy>();
            enemyComponent?.DealDemage(Strength, this.demageType);                       // Nur, wenn das Objekt nicht null ist, wird diese Funktion aufgerufen.
            Destroy(gameObject);
            // TODO deal demage to enemy
        }
        
        transform.Translate(0, 0, distanceThisFrame);
    }

    private void LifeTimeHandling(){
        if (RemainingLifeTime <= 0){
            Destroy(gameObject);
        }

        RemainingLifeTime -= Time.deltaTime;
    }
}
