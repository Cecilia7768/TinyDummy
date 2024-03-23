using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class IsAttacked : Conditional
    {
        private IUnitService unitService;
        private NavMeshAgent agent;

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();
            unitService = this.transform.parent.GetComponent<IUnitService>();
        }
        public override TaskStatus OnUpdate()
        {
            if (unitService.GetIsAttacked())
            {
                agent.isStopped = true;
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }


    }
}
