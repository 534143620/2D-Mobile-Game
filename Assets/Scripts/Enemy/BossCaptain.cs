using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCaptain : Enemy, IDamageable
{
    public Rigidbody2D rb;
    public GameObject sword;
    private Transform ShotPoint;
    //private PlayerController playerController;

    public override void Init()
    {
        base.Init();
        rb = GetComponent<Rigidbody2D>();
        ShotPoint = transform.Find("ShotPoint");
        //playerController = GetComponent<PlayerController>();
        //anim = GetComponent<Animator>();
    }

    public override void SkillAction()
    {
        base.SkillAction();
    }

    public override void Update()
    {
        base.Update();
        if (Mathf.Abs(transform.position.x - pointA.position.x) < 0.02f || 
            Mathf.Abs(transform.position.x - pointB.position.x) < 0.02f )
        {
            AutoJump();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            GameObject gameObject_sword = Instantiate(sword, ShotPoint.position, Quaternion.identity);
        }
    }

    public override void UpdatePointAandB()
    {
        
    }

    public void AutoJump()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            anim.Play("Jump");
            rb.AddForce(Vector3.up * 6.0f, ForceMode2D.Impulse);
        }
    }

    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
    }
}
