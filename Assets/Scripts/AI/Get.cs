using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using UnityEngine.AI;

namespace AI
{
    public class Get : Action
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

            if (distanceToTarget <= 15f)
            {
                agent.isStopped = true;
                return TaskStatus.Success;
            }
            else if (CanSeeObject.targetObject.Value != null)
                agent.SetDestination(CanSeeObject.targetObject.Value.transform.position);

            return TaskStatus.Running;
        }
        
    }
}
