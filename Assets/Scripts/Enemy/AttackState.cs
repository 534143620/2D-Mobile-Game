using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        //Debug.Log("发现敌人");
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (enemy.attackList.Count == 0)
            enemy.transitionToState(enemy.patrolState);
        if (enemy.attackList.Count > 0)
        {
            for (int i = 0; i < enemy.attackList.Count; i++)
            {
                if (Mathf.Abs(enemy.transform.position.x - enemy.attackList[i].position.x)
                    < Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x))
                {
                    enemy.targetPoint = enemy.attackList[i];
                }
                   
            }
        }

        if (enemy.targetPoint.CompareTag("Player"))
        {
            enemy.attackAction();
        }
        else if(enemy.targetPoint.CompareTag("Bomb"))
        {
            enemy.skillAction();
        }
        enemy.moveToTarget();
    }
}
