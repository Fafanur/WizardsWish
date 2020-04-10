using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemybehaviour : MonoBehaviour
{
    public enum EnemyState
    {
        Move,
        PrepareAttack,
        Attacking,
        Dead
    }
    public EnemyState curState;

    private Transform target;
    private Vector3 attackPosition;

    public ObjectHolder objectHolder;
    public float attackDistance;

    public BoxCollider attackCollider;
    public BoxCollider regularCollider;
    private Rigidbody rb;
    
    public NavMeshAgent agent;
    public Health healthComp;
    public GameObject Item;

    public float offset;
    public float attackDamage;
    private void Awake()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        healthComp.deathEvent += EnemyDeath;
        SetState(EnemyState.Move);
    }
    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        switch (curState)
        {
            case EnemyState.Move:
                MoveTowardsTarget();
                if (dist <= attackDistance)
                {
                    attackPosition = target.position + (transform.position - target.position).normalized * attackDistance;
                    attackPosition.y = target.position.y;
                    SetState(EnemyState.PrepareAttack);
                }
                break;
            case EnemyState.PrepareAttack:
                agent.SetDestination(attackPosition);
                if (!agent.hasPath || agent.pathStatus != NavMeshPathStatus.PathComplete)
                {
                    SetState(EnemyState.Move);
                }
                else if (Vector3.Distance(attackPosition, transform.position) <= 2f)
                {
                    objectHolder.enabled = false;
                    Attack();
                    SetState(EnemyState.Attacking);
                }
                break;
            case EnemyState.Dead:
                agent.isStopped = true;
                break;
        }
    }

    private void SetState(EnemyState state)
    {
        curState = state;
    }

    private void AttackAnimationEnd()
    {
        attackCollider.enabled = false;
        agent.transform.position = transform.position;
        SetState(EnemyState.Move);
    }

    private void EnemyDeath(Health healthComp)
    {
        SetState(EnemyState.Dead);
        this.healthComp.deathEvent -= EnemyDeath;
        objectHolder.enabled = false;
        attackCollider.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        transform.DOKill();
        rb.AddForce((-transform.forward + transform.up) * 5f, ForceMode.Impulse);
        rb.AddTorque((Vector3.right + Vector3.up) * 10f);
        Invoke(nameof(DestroyMe), 10f);
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        transform.DOKill();
        healthComp.deathEvent -= EnemyDeath;
    }

    private void SpawnItem()
    {
        
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 diff = targetPosition - transform.position;
        targetPosition = transform.position + diff.normalized * (diff.magnitude - offset);

        return targetPosition;
    }
    private void MoveTowardsTarget()
    {
        objectHolder.enabled = true;
        agent.SetDestination(GetTargetPosition());
    }
    private void Attack()
    {
        attackCollider.enabled = true;
        Vector3 targetPosition = GetTargetPosition();
        if (target.position.y < transform.position.y)
        {
            targetPosition.y = transform.position.y;
        }
        transform.DOLookAt(target.transform.position, 1f, AxisConstraint.Y, Vector3.up);
        transform.DOMove(targetPosition, 2f).SetEase(Ease.OutBack).OnComplete(AttackAnimationEnd).SetDelay(1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && curState == EnemyState.Attacking)
        {
            Health healthComp = collision.gameObject.GetComponent<Health>();
            if (healthComp != null)
            {
                healthComp.DoDamage(attackDamage);
            }

        }
    }
}
