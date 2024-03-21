using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class IsGoToSpawn : Conditional
    {
        private NavMeshAgent agent;
        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();

            agent.destination = EnvironmentManager.Instance.nestPosi.transform.position;
            agent.isStopped = false;
        }

        public override TaskStatus OnUpdate()
        {
            //도착했으면
            if(HasReachedDestination())
                return TaskStatus.Success;
            return TaskStatus.Running;
        }

        bool HasReachedDestination()
        {
            if (!agent.pathPending) // 경로 계산이 완료되었는지 확인
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}