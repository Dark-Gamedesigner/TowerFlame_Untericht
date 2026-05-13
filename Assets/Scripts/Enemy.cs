using UnityEngine;

public class Enemy : MonoBehaviour{
    public EnemyPathControll Target;

    public float Speed;

    public float TurnSpeed = 1080;
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
        Vector3 directionToTarget = Target.transform.position - transform.position;
        directionToTarget.Normalize();
        float rightDot = Vector3.Dot(transform.right,directionToTarget);
        if (Mathf.Abs(rightDot)< 0.05){
            transform.LookAt(Target.transform.position);
        }
        else{
            float sign = rightDot > 0 ? 1 : -1;
            transform.Rotate(0, sign * TurnSpeed * Time.deltaTime, 0);
        }
        transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
        if (Vector3.Distance(transform.position, Target.transform.position) <= 0.1f){
            // Wenn wir grob auf dem Ziel sind:
            if (Target.isEnd){
                Destroy(gameObject);
            }
            // Wenn wir auf dem Ziel sind:
            Target = Target.next;    // Ändere das Ziel zum nächsten
            //transform.LookAt(Target.transform.position); Drehe zum nächsten Ziel
        }
    }
}
