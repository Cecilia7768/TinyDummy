using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;

public class IsThirst : Conditional
{
    private IUnitService unitService;

    public override void OnStart()
    {
        unitService = this.gameObject.GetComponent<IUnitService>();
    }

    /// <summary>
    /// 목마르면 한잔
    /// </summary>
    /// <returns></returns>
    public override TaskStatus OnUpdate()
    {
        return unitService.GetThirst() < 80 ? TaskStatus.Success : TaskStatus.Failure;
    }
}
