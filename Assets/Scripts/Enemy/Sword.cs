using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public float speed ;
    public float lifeTime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    public GameObject player;
    public Vector2 MoveToVector;
    private float startTime;
    //public GameObject destroyEffect;

    public void Start()
    {
        //Invoke("DestroyProjectile", lifeTime);
        player = FindObjectOfType<PlayerController>().gameObject;
        MoveToVector = player.transform.position - transform.position;
        startTime = Time.time;
    }

    public void Update()
    {
        if (Time.time > startTime + lifeTime )
        {
            DestroyProjectile();
            return;
        }
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                //hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                hitInfo.collider.GetComponent<IDamageable>().GetHit(1);
            }
        }
        transform.Translate(MoveToVector.normalized * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
