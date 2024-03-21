using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using Definition;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class GetFood : Action
    {
        private NavMeshAgent agent;

        public override void OnStart()
        {
            base.OnStart();
            agent = GetComponent<NavMeshAgent>();
            agent.isStopped = false;
        }

        public override TaskStatus OnUpdate()
        {
            if (CanSeeObject.targetObject.Value == null || CanSeeObject.targetObject.Value.tag != Tags.FOOD)
            {
                return TaskStatus.Failure; 
            }

            float distanceToTarget = Vector3.Distance(agent.transform.position, CanSeeObject.targetObject.Value.transform.position);
            if (distanceToTarget <= 13f)
            {
                agent.isStopped = true; 
                return TaskStatus.Success;
            }
            else
            {
                if (agent.isStopped || !agent.hasPath)
                {
                    agent.isStopped = false;
                    agent.SetDestination(CanSeeObject.targetObject.Value.transform.position); 
                }
                return TaskStatus.Running;
            }
        }
    }
}
