using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : Enemy
{
    public int health;

    public override void Init()
    {
        base.Init();
    }

    public override void skillAction()
    {
        base.skillAction();
    }

    public void SetOff() //Animation Event
    {
        //if (targetPoint)
        //{
        //    targetPoint.GetComponent<Bomb>().TurnOff();
        //}
        //问号‘?’的作用表示判断是否为空
        targetPoint.GetComponent<Bomb>()?.TurnOff();
    }

}
