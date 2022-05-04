using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private float attackTime = 3.0f;
    private float startAttackTime;
    public GameObject sword;
    private Transform ShotPoint;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startAttackTime = Time.time;
        ShotPoint = transform.Find("ShotPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.EnemyDeadAll() || GameManager.instance.GameOver)
            return;
        if (Time.time > attackTime + startAttackTime)
        {
            anim.Play("Cannon_attack");
            GameObject gameObject_sword = Instantiate(sword, ShotPoint.position, Quaternion.identity);
            startAttackTime = Time.time;
        }

    }
}
