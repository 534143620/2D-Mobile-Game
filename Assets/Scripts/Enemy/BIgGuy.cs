using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIgGuy : Enemy,IDamageable
{
    public Transform pickupPoint;
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

    public void PickUpBomb() //Animation Event
    {
        if (targetPoint.CompareTag("Bomb") && !hasBomb)
        {
            targetPoint.position = pickupPoint.position;
            targetPoint.SetParent(pickupPoint);
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            hasBomb = true;
        }

    }

    public void ThrowAway() //Animation Event
    {
        if (hasBomb)
        {
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            targetPoint.SetParent(transform.parent.parent);

            if (FindObjectOfType<PlayerController>().gameObject.transform.position.x < transform.position.x)
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * 8, ForceMode2D.Impulse);
            else
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * 8, ForceMode2D.Impulse);
        }
        hasBomb = false;
    }
}
