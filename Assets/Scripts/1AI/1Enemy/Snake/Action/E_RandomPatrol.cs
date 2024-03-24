using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Enemy.AI
{
    public class E_RandomPatrol : Action
    {
        private NavMeshAgent agent;
        private float timer;

        private bool isInitSet = false; // 초기설정

        private IEnemyService enemyService;

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();
            enemyService = this.transform.GetComponent<IEnemyService>();

            if (enemyService != null && !isInitSet)
            {
                timer = enemyService.GetPatrolTimer(); // 타이머 초기화
                isInitSet = true;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (enemyService == null)
            {
                return TaskStatus.Failure;
            }

            timer += Time.deltaTime;

            if (timer >= enemyService.GetPatrolTimer())
            {
                Vector3 newPos;
                if (TryGetRandomNavSphere(transform.position, enemyService.GetPatrolRadius(), -1, out newPos))
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