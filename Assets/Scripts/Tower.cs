using UnityEngine;

public class Tower : MonoBehaviour
{
    //ToDo Gegner erkennen
    //ToDo zum Gegner Drehen
    //ToDo Schießen (Projektil Spawnen)
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
       var ColliderInRange = Physics.OverlapSphere(transform.position, Range);
       Enemy target;
       foreach (var collider in ColliderInRange){
           Enemy componentOfTarget = collider.GetComponent<Enemy>();
           if (componentOfTarget != null){
               target = componentOfTarget;
               Shoot(target);
               break;
           }
       }
    }

    private void Shoot(Enemy target){
        var newProjectile = Instantiate(Projectil, transform.position, Quaternion.identity);
        newProjectile.transform.LookAt(target.transform.position);
        Rigidbody projectilesRB = newProjectile.GetComponent<Rigidbody>();
        if (projectilesRB != null){
            projectilesRB.linearVelocity = newProjectile.transform.forward * 5;
        }
    }
}
