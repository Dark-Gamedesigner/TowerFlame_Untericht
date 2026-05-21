using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //ToDo Priority
    //ToDo wert wie Stärke, Schussrate; Reichweite etc.
    public TowerStats stats;

    [SerializeField]private float _attackCoolDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){ }

    // Update is called once per frame
    void Update(){
        _attackCoolDown -= Time.deltaTime;
        if (_attackCoolDown <= 0){
            var target = AcquireFirstTarget();
            if (target){ //Implizierter null check
                transform.LookAt(target.transform.position);
                Shoot(target);

            }
            else{
                _attackCoolDown += 0.1f;
                // Als Optimierungsmaßnahme prüft ein einzelner Turm nur maximal 10 Mal die Sekunde nach Zielen,
                // wenn zuvor keines gefunden wurde. 
            }
        }
    }

    private Enemy AcquireFirstTarget(){
        // Todo: Mit Layern Kollisionserkennung optimieren.
        var collidersInRange = Physics.OverlapSphere(transform.position, stats.Range, LayerMask.GetMask("Enemies"));
        Enemy targetCandidate = null;
        // Erstmal suchen wir den nächstbesten Enemy
        foreach (var collider in collidersInRange){
            if (collider.GetComponent<Enemy>() != null){
                targetCandidate = collider.GetComponent<Enemy>();
                break;
            }
        }
        // Wenn kein Ziel gefunden wurde, gebe null zurück um die Funktion früh abzubrechen.
        if (targetCandidate == null) return targetCandidate;
        // Ab jetzt ist targetCandidate garantiert definiert.
        // Jetzt vergleichen wir, welches der beste Kandidat ist.
        foreach (var collider in collidersInRange){
            Enemy componentOfTarget = collider.GetComponent<Enemy>();
            if (componentOfTarget != null){
                if (componentOfTarget.DistanceTravelled > targetCandidate.DistanceTravelled){
                    // Wenn der betrachtete Gegner weiter gereist wird, wird dieser zum neuen Kandidaten.
                    targetCandidate = componentOfTarget;
                }
            }
        }
        // AB JETZT ist unser Kandidat optimal
        return targetCandidate;
    }

        private void Shoot(Enemy target){
            var newProjectile = Instantiate(stats.Projectile, transform.position, Quaternion.identity);
            var projectilePosition = newProjectile.transform.position;
            projectilePosition.y = target.transform.position.y;
            newProjectile.transform.position = projectilePosition;
            newProjectile.transform.LookAt(target.transform.position);
            _attackCoolDown = 1 / Mathf.Max(stats.FireRate, 0.001f);
        }

        private void OnDrawGizmos(){
            Gizmos.DrawWireSphere(transform.position, stats.Range);
        }
    }

