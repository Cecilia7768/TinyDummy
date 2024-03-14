using UnityEngine;
using UnityEngine.AI;
using Definition;
using BehaviorDesigner.Runtime.Tasks;

namespace AI
{
    public class RandomPatrol : Action
    {
        private NavMeshAgent agent;
        private float timer;

        private bool isInitSet = false; // 초기설정

        private IUnitService unitService;

        public override void OnStart()
        {
            base.OnStart();

            agent = GetComponent<NavMeshAgent>();
            agent.isStopped = false;

            unitService = this.transform.parent.GetComponent<IUnitService>();

            if (unitService != null && !isInitSet)
            {
                timer = unitService.GetPatrolTimer(); // 타이머 초기화
                isInitSet = true;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (unitService == null)
            {
                return TaskStatus.Failure;
            }

            timer += Time.deltaTime;

            if (timer >= unitService.GetPatrolTimer())
            {
                Vector3 newPos;
                if (TryGetRandomNavSphere(transform.position, unitService.GetPatrolRadius(), -1, out newPos))
                {
                    agent.SetDestination(newPos);
                    timer = 0; // 타이머 재설정
                    return TaskStatus.Success;
                }
            }

            return TaskStatus.Running;
        }

        private bool TryGetRandomNavSphere(Vector3 origin, float dist, int layermask, out Vector3 result)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;
            randDirection += origin;

            NavMeshHit navHit;
            if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
            {
                result = navHit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }

    }
}
