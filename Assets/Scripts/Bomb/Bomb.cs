using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    private CircleCollider2D coll;
    private Rigidbody2D rb;
    public float startTime;
    public float waitTime;
    public float bombForce;
    public float damage;

    [Header("Check")]
    public float radius;
    public LayerMask targetLayer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        damage = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("bomb_off"))
        {
            return;
        }
        if (Time.time > startTime + waitTime)
        {
            anim.Play("bomb_explosion");
        }
    }

    public void Explosion()//爆照动画的第一针增加爆炸事件
    {
        coll.enabled = false;
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius,targetLayer);
        rb.gravityScale = 0;
        foreach (var item in aroundObjects)
        {
            Vector3 pos = transform.position - item.transform.position;
            item.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * bombForce, ForceMode2D.Impulse);
            if (item.CompareTag("Bomb") && item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bomb_off"))
            {
                item.GetComponent<Bomb>().TurnOn();
            }
            if (item.CompareTag("Player"))
            {
                item.GetComponent<IDamageable>().GetHit(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void TurnOff()
    {
        anim.Play("bomb_off");
        gameObject.layer = LayerMask.NameToLayer("NPC");
    }

    public void TurnOn()
    {
        startTime = Time.time;
        anim.Play("bomb_on");
        gameObject.layer = LayerMask.NameToLayer("Bomb");
    }
}
