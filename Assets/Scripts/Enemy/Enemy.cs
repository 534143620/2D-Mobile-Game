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
    public bool hasBomb;
    public bool isBoss;

    [Header("Movement")]
    public float speed;
    public float attackPosinRange;
    public Transform targetPoint;

    [Header("Attack Setting")]
    public float attackRate;
    private float nextAttack = 0;
    public float attackRange, skillRange;

    public List<Transform> attackList = new List<Transform>();

    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();

    public Transform pointA;
    public Transform pointB;
    public float orgY;
    public float autoSwitchPoint;
    private float attackPosin;


    public void Awake()
    {
        Init();
    }

    public void Start()
    {
        TransitionToState(patrolState);
        if (isBoss)
            UIManager.instance.SetBossHealth(health);
        GameManager.instance.IsEnemy(this);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isBoss)
            UIManager.instance.UpdateBossHealth(health);
        //人物优化，防止敌人位置下落后，不改变pointA造成的卡顿
        UpdatePointAandB();
        anim.SetBool("dead", isDead);
        if (isDead)
        {
            GameManager.instance.EnemyDead(this);
            return;
        }
        currentState.OnUpdate(this);
        anim.SetInteger("state", animState);
    }

    public virtual void UpdatePointAandB()
    {
          //人物优化，防止敌人位置下落后，不改变pointA造成的卡顿
        if (Math.Abs(transform.position.y - orgY) > 0.3f)
        {
            pointB.position = transform.position + new Vector3(attackPosin, 0, 0);
            pointA.position = transform.position + new Vector3(-attackPosin, 0, 0);
            orgY = transform.position.y;
        }
    }

    public virtual void Init()
    {
        anim = GetComponent<Animator>();
        orgY = transform.position.y;
        alarmSign = transform.GetChild(0).gameObject;
        attackPosin = Math.Abs(transform.position.x - pointA.position.x);
        if (isBoss)
            health = 15;
        else
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

    public void SwitchPointForPatrol()
    {
        if (targetPoint == pointA)
        {
            targetPoint = pointB;
            return;
        }
        if (targetPoint == pointB)
        {
            targetPoint = pointA;
            return;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (attackList.Contains(collision.transform) || hasBomb || GameManager.instance.GameOver)
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
        if (isDead|| GameManager.instance.GameOver)
            return;
        StartCoroutine(OnAlarm()); 
    }

    IEnumerator OnAlarm()
    {
        alarmSign.SetActive(true);
        yield return new WaitForSeconds(alarmSign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        alarmSign.SetActive(false);
    }
}
