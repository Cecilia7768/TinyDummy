using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using UnityEngine.AI;
using Definition;

namespace AI
{
    public class GetFood : Action
    {
        private NavMeshAgent agent;

        public override void OnStart()
        {
            base.OnStart();
            agent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            float distanceToTarget = Vector3.Distance(agent.transform.position, CanSeeObject.targetObject.Value.transform.position);

             if (CanSeeObject.targetObject.Value.tag == Tags.FOOD)
            {
                if (distanceToTarget <= 10f)
                {
                    agent.isStopped = true;
                    return TaskStatus.Success;
                }
                else if (CanSeeObject.targetObject.Value != null)
                    agent.SetDestination(CanSeeObject.targetObject.Value.transform.position);
            }

            return TaskStatus.Failure;
        }

    }
}
