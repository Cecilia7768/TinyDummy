using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using Definition;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class GetDrink : Action
    {
        private NavMeshAgent agent;
        private Vector3 lastPosition;
        private float stationaryTime = 0f;

        public override void OnStart()
        {
            base.OnStart();
            agent = GetComponent<NavMeshAgent>();
            agent.isStopped = false;
            lastPosition = transform.position;
        }

        public override TaskStatus OnUpdate()
        {
            if (CanSeeObject.targetObject.Value == null || CanSeeObject.targetObject.Value.tag != Tags.DRINK)
            {
                return TaskStatus.Failure;
            }

            float distanceToTarget = Vector3.Distance(transform.position, CanSeeObject.targetObject.Value.transform.position);

            // 목적지에 도착했을 경우
            if (distanceToTarget <= 17f)
            {
                agent.isStopped = true;
                return TaskStatus.Success;
            }
            else
            {
                if (Vector3.Distance(transform.position, lastPosition) <= 0.1f)
                {
                    stationaryTime += Time.deltaTime;

                    // 2초 동안 위치 변화가 없다면, 다시 목적지 설정
                    if (stationaryTime >= 2f)
                    {
                        agent.SetDestination(CanSeeObject.targetObject.Value.transform.position);
                        stationaryTime = 0f;
                    }
                }
                else
                {
                    stationaryTime = 0f;
                    lastPosition = transform.position;
                }

                // 계속해서 목적지를 향해 이동
                if (!agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath || agent.velocity.sqrMagnitude == 0f))
                {
                    agent.SetDestination(CanSeeObject.targetObject.Value.transform.position);
                }

                return TaskStatus.Running;
            }
        }
    }
}