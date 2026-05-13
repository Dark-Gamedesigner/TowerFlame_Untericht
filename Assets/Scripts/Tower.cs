using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //ToDo Priority
    //ToDo wert wie Stärke, Schussrate; Reichweite etc.

    public float Range;
    public float FireRate;

    private float _attackCoolDown;

    public GameObject Projectil;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        _attackCoolDown -= Time.deltaTime;
        if (_attackCoolDown <= 0){
            var ColliderInRange = Physics.OverlapSphere(transform.position, Range);
            Enemy targetCandidate;
            // Erstmal suchen wir den nächsten Enemy
            foreach (var collider in ColliderInRange){
                if (collider.GetComponent<Enemy>() != null){
                    targetCandidate = collider.GetComponent<Enemy>();
                    break;
                }
            }
            
            foreach (var collider in ColliderInRange){
                Enemy componentOfTarget = collider.GetComponent<Enemy>();
                if (componentOfTarget != null){
                    if (componentOfTarget.DistanceTravelled > targetCandidate.DistanceTravelled){
                        targetCandidate = componentOfTarget;
                    }
                }
            }
        }
    }

    private void Shoot(Enemy target){
        var newProjectile = Instantiate(Projectil, transform.position, Quaternion.identity);
        var projectilePosition = newProjectile.transform.position;
        projectilePosition.y = target.transform.position.y;
        newProjectile.transform.position = projectilePosition;
        newProjectile.transform.LookAt(target.transform.position);
        _attackCoolDown = 1 / Mathf.Max(FireRate, 0.001f);
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position.Range);
    }
}
