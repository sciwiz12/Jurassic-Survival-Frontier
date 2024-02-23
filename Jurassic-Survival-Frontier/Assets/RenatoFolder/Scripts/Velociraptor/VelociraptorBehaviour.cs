using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VelociraptorBehaviour : MonoBehaviour
{
    [SerializeField] private Detection detection;
    private Transform t;
    private NavMeshAgent navAgent;
    //private Transform target;
    private Animator animator;
    private int velocityHash;
    private Vector3 beforeAttack;

    // Values
    private float velocityState = 0f;
    [SerializeField]
    private float speedRotation = 100.0f;
    [SerializeField]
    private float distanceToAttack = 4f;
    [SerializeField]
    private float speed = 10f;
    // States
    private float idle = 0f;
    private float walk = 0.5f;
    private float run = 1f;

    // Start is called before the first frame update
    void Start()
    {
        t = this.transform;
        navAgent = GetComponent<NavMeshAgent>();
        //target = gameObject.GetComponentInParent<EnemyUtils>().playerPos;
        animator = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }

    void Chase()
    {
        if (detection.targetDetected)
        {
            if ((detection.target.position - t.position).magnitude >= distanceToAttack)
            {
                Run();
                Rotation();
            }
            else
            {
                Aim();
                Attack();
            }
        }
        else
        {
            navAgent.SetDestination(t.position);
            Idle();
        }
    }
    void Rotation()
    {
        Quaternion current = transform.localRotation;
        Quaternion rotation = Quaternion.LookRotation(t.position - detection.target.position);
        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * speedRotation);
    }

    void Idle()
    {
        velocityState = idle;
        animator.SetFloat(velocityHash, velocityState);
    }

    void Run()
    {
        //animator.ResetTrigger("Aim");
        //animator.ResetTrigger("Attack");
        Vector3 objective = detection.target.position;
        navAgent.SetDestination(objective);
        velocityState = run;
        animator.SetFloat(velocityHash, velocityState);
    }

    void Aim()
    {
        navAgent.SetDestination(t.position);
        animator.SetTrigger("Aim");
        Rotation();
    }

    void Attack()
    {
        beforeAttack = t.position;
        animator.SetTrigger("Attack");
    }

    void ResetAttackTrigger()
    {
        animator.ResetTrigger("Attack");
    }
}
