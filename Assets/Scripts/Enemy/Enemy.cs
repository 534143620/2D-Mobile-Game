using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyBaseState currentState;

    public Animator anim;
    public int animState;
    private GameObject alarmSign;
    [Header("Enemy State")]
    public float health;
    public bool isDead;

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
        TransitionToState(patrolState);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        anim.SetBool("dead", isDead);
        if (isDead)
            return;
        currentState.OnUpdate(this);
        anim.SetInteger("state", animState);
    }

    public virtual void Init()
    {
        anim = GetComponent<Animator>();
        alarmSign = transform.GetChild(0).gameObject;
        health = 10;
    }

    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        FileDirection();
    }

    public void AttackAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)
        {
            if (Time.time > nextAttack)
            {
                //执行攻击
                anim.SetTrigger("attack");
                Debug.Log("普通攻击");
                nextAttack = Time.time + attackRate;
            }
        }
    }

    public virtual void SkillAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {
            if (Time.time > nextAttack)
            {
                //执行攻击
                anim.SetTrigger("skill");
                Debug.Log("执行技能攻击");
                nextAttack = Time.time + attackRate;
            }
        }

    }

    public void FileDirection()
    {
        if (transform.position.x < targetPoint.position.x)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }   

    public void SwitchPoint()
    {
        if (Mathf.Abs(pointB.position.x - transform.position.x) < Mathf.Abs(pointA.position.x - transform.position.x))
            targetPoint = pointA;
        else
            targetPoint = pointB;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (attackList.Contains(collision.transform))
            return;
        else
            attackList.Add(collision.transform);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(OnAlarm());
    }

    //private void StartCoroutine(IEnumerable enumerable)
    //{
    //    throw new NotImplementedException();    //}

    IEnumerator OnAlarm()
    {
        alarmSign.SetActive(true);
        yield return new WaitForSeconds(alarmSign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        alarmSign.SetActive(false);
    }
}
