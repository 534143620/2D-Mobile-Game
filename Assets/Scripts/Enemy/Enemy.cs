using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyBaseState currentState;

    public Animator anim;
    public int animState;

    [Header("Movement")]
    public float speed;
    public Transform pointA, pointB;
    public Transform targetPoint;

    [Header("Attack Setting")]
    public float attackRate;
    private float nextAttack = 0;
    public float attackRange, skillRange;

    public List<Transform> attackList = new List<Transform>();

    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();
    // Start is called before the first frame update

    public void Awake()
    {
        Init();
    }
    void Start()
    {
        //targetPoint = pointA;
        transitionToState(patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate(this);
        anim.SetInteger("state", animState);
    }

    public virtual void Init()
    {
        anim = GetComponent<Animator>();
    }

    public void transitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void moveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        fileDirection();
    }

    public void attackAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)
        {
            if (Time.time > nextAttack)
            {
                //执行攻击
                Debug.Log("普通攻击");
                nextAttack = Time.time + attackRate;
            }
        }
    }

    public virtual void skillAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {
            if (Time.time > nextAttack)
            {
                //执行攻击
                nextAttack = Time.time + attackRate;
            }
        }

    }

    public void fileDirection()
    {
        if (transform.position.x < targetPoint.position.x)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }   

    public void switchPoint()
    {
        if (Mathf.Abs(pointB.position.x - transform.position.x) < Mathf.Abs(pointA.position.x - transform.position.x))
            targetPoint = pointA;
        else
            targetPoint = pointB;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attackList.Contains(collision.transform))
            return;
        else
            attackList.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);
    }
}
