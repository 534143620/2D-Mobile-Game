using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        //Debug.Log("发现敌人");
        enemy.animState = 2;
        enemy.targetPoint = enemy.attackList[0];
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (enemy.attackList.Count == 0)
            enemy.TransitionToState(enemy.patrolState);
        else if (enemy.attackList.Count > 0)
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
        else enemy.targetPoint = enemy.attackList[0];

        //if (enemy.attackList.Count == 1)
        //    enemy.targetPoint = enemy.attackList[0];

        if (enemy.targetPoint.CompareTag("Player"))
        {
            enemy.AttackAction();
        }
        if(enemy.targetPoint.CompareTag("Bomb"))
        {
            enemy.SkillAction();
        }
        enemy.MoveToTarget();
    }
}
