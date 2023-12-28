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
            if (CanSeeObject.targetObject.Value != null)
            {
                agent.SetDestination(CanSeeObject.targetObject.Value.transform.position);

                // 에이전트와 타겟 오브젝트 사이의 거리를 계산
                float distanceToTarget = Vector3.Distance(agent.transform.position, CanSeeObject.targetObject.Value.transform.position);

                // 거리가 1 이하이면 타겟 오브젝트를 비활성화
                if (distanceToTarget <= 3.5f)
                {
                    CanSeeObject.targetObject.Value.SetActive(false);
                    return TaskStatus.Success;
                }
            }

            return TaskStatus.Running;
        }
    }
}
