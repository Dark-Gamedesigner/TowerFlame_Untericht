using UnityEngine;

public class Enemy : MonoBehaviour{
    public EnemyPathControll Target;
    
    public float DistanceTravelled{ get; private set; }
    public float Speed;
    public float TurnSpeed = 1080;
    private int _health = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Target != null){
            transform.LookAt(Target.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null) return;
        var rightDot = GetTargetRightDot();
        RotateByRightDot(rightDot);
        MoveForward();
        CheckArrival();
    }

    private void CheckArrival(){
        if (Vector3.Distance(transform.position, Target.transform.position) <= 0.1f){
            // Wenn wir grob auf dem Ziel sind:
            if (Target.isEnd){
                Destroy(gameObject);
            }
            else{
                // Wenn wir auf dem Ziel sind:
                Target = Target.next;    // Ändere das Ziel zum nächsten
                //transform.LookAt(Target.transform.position); Drehe zum nächsten Ziel
            }
        }
    }

    private void MoveForward(){
        float stepThisFrame = (Speed * Time.deltaTime);                                 // Die Distanz, die wir disen Frame laufen werden.
        DistanceTravelled += stepThisFrame;                                             // Im "Schrittzähler" vermerken
        transform.Translate(Vector3.forward * stepThisFrame);       // Vorwärts bewegen um diese Distanz 
    }

    private void RotateByRightDot(float rightDot){
        if (Mathf.Abs(rightDot)< 0.05){
            transform.LookAt(Target.transform.position);
        }
        else{
            float sign = rightDot > 0 ? 1 : -1;
            transform.Rotate(0, sign * TurnSpeed * Time.deltaTime, 0);
        }
    }

    private float GetTargetRightDot(){
        Vector3 directionToTarget = Target.transform.position - transform.position;
        directionToTarget.Normalize();
        float rightDot = Vector3.Dot(transform.right,directionToTarget);
        return rightDot;
    }

    public void DealDemage(int incomingDemage){
        int wouldBeHealth = _health - incomingDemage;
        if (wouldBeHealth < 0){
            wouldBeHealth = 0;
        }

        if (wouldBeHealth == 0){
            Destroy(gameObject);
            return;
        }

        _health = wouldBeHealth;
    }
}
