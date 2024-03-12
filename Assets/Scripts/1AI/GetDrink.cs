using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using UnityEngine.AI;
using Definition;

namespace AI
{
    public class GetDrink : Action
    {
        private NavMeshAgent agent;

        public override void OnStart()
        {
            base.OnStart();
            agent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            if (!agent.pathPending && !agent.hasPath && agent.velocity.sqrMagnitude == 0f)
            {
                agent.isStopped = false;
                agent.SetDestination(CanSeeObject.targetObject.Value.transform.position);
            }

            float distanceToTarget = Vector3.Distance(agent.transform.position, CanSeeObject.targetObject.Value.transform.position);

            if (CanSeeObject.targetObject.Value.tag == Tags.DRINK)
            {
                if (distanceToTarget <= 15f)
                {
                    agent.isStopped = true;
                    return TaskStatus.Success;
                }
                else
                {
                    agent.SetDestination(CanSeeObject.targetObject.Value.transform.position);
                    return TaskStatus.Running;
                }
            }

            return TaskStatus.Failure;
        }

    }
}