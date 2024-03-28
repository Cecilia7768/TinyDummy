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
        private float lastPathCheckTime; // 마지막으로 경로를 확인한 시간
        private Vector3 lastPosition; // 마지막으로 기록된 위치
        private const float checkDuration = 2f; // 위치 변경을 확인할 시간 간격 (초)
        private const float minDistanceChange = 1f; // "같은 위치"로 판단할 최소 거리 변화

        public override void OnStart()
        {
            base.OnStart();
            agent = GetComponent<NavMeshAgent>();
            agent.isStopped = false;
            lastPosition = transform.position;
            lastPathCheckTime = Time.time;
        }

        public override TaskStatus OnUpdate()
        {
            if (CanSeeObject.targetObject == null || CanSeeObject.targetObject.Value == null || CanSeeObject.targetObject.Value.tag != Tags.FOOD)
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
                // 현재 위치와 마지막 위치의 거리가 매우 작고, 설정된 시간이 경과했는지 확인
                if ((Time.time - lastPathCheckTime >= checkDuration) &&
                    (Vector3.Distance(transform.position, lastPosition) <= minDistanceChange))
                {
                    // 위치가 거의 변하지 않았다면, 다시 목적지를 설정
                    agent.SetDestination(CanSeeObject.targetObject.Value.transform.position);
                    lastPathCheckTime = Time.time;
                }

                // 현재 위치 업데이트
                lastPosition = transform.position;

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
