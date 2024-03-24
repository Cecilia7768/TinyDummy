using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class IsFoundEnemy : Conditional
    {
        private IUnitService unitService;

        public override void OnStart()
        {
            unitService = this.transform.parent.GetComponent<IUnitService>();
        }
        public override TaskStatus OnUpdate()
        {
            if (unitService.GetIsFoundEnemy())
                return TaskStatus.Success;
            return TaskStatus.Failure;
        }
    }
}