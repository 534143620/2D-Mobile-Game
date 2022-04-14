using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bald :Enemy ,IDamageable
{
    public override void Init()
    {
        base.Init();
    }

    public override void SkillAction()
    {
        base.SkillAction();
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
