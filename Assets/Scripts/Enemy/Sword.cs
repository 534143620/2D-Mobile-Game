using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public float speed ;
    public float lifeTime;
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
        transform.Translate(MoveToVector.normalized * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(1);
        }
    }

    void DestroyProjectile()
    {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
