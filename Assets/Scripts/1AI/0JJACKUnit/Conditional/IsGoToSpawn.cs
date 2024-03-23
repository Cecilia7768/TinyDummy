using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class IsGoToSpawn : Conditional
    {
        private NavMeshAgent agent;
        private Vector3 previousPosition;
        private float stationaryTimer = 0f;
        public float stationaryThreshold = 0.1f; // 거의 움직이지 않는 것으로 간주되는 최대 거리
        public float resetPathTime = 2f; // 경로를 다시 계산하기 전에 멈춰있어야 하는 시간

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();
            // 자유로운 산란 -> 잠시 주석
            // agent.destination = EnvironmentManager.Instance.nestPosi.transform.position;
            agent.isStopped = false;
            previousPosition = transform.position;
        }

        public override TaskStatus OnUpdate()
        {
            if (HasReachedDestination())
            {
                return TaskStatus.Success;
            }

            // 이전 위치와 현재 위치 사이의 거리가 매우 작다면, 에이전트가 거의 움직이지 않고 있다고 간주
            if (Vector3.Distance(previousPosition, transform.position) <= stationaryThreshold)
            {
                stationaryTimer += Time.deltaTime;
                // 일정 시간 동안 거의 움직이지 않았다면, 경로를 다시 계산
                if (stationaryTimer >= resetPathTime)
                {
                   // 자유로운 산란 -> 잠시 주석
                   // agent.SetDestination(EnvironmentManager.Instance.nestPosi.transform.position);
                    stationaryTimer = 0f;
                    Debug.LogError("Spawn 끼임. 위치 수정함");
                }
            }
            else
            {
                // 위치가 변했다면 타이머와 이전 위치를 리셋
                stationaryTimer = 0f;
                previousPosition = transform.position;
            }

            return TaskStatus.Running;
        }

        bool HasReachedDestination()
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
            {
                return true;
            }
            return false;
        }
    }
}