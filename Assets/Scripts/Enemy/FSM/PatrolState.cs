using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 0;
        enemy.SwitchPoint();
        enemy.autoSwitchPoint = Time.time;
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            enemy.animState = 1;
            enemy.MoveToTarget();
            //enemy.MoveForPatrolStare();
        }
        //如果在巡逻状态，5秒没有到达enemy.targetPoint则自动转换位置
        if (Time.time > enemy.autoSwitchPoint + 5)
        {
            enemy.autoSwitchPoint = Time.time + 5;
            enemy.SwitchPointForPatrol();
        }

        if (Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x) < 0.01f)
        {
            enemy.TransitionToState(enemy.patrolState);
        }
        
        if (enemy.attackList.Count > 0)
            enemy.TransitionToState(enemy.attackState);

    }
}
