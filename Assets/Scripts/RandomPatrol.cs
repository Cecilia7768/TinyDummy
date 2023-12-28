using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;

namespace AI
{
    public class RandomPatrol : Action
    {
        private NavMeshAgent agent;
        private float timer;

        private bool isInitSet = false; // �ʱ⼳��

        public override void OnStart()
        {
            base.OnStart();

            agent = GetComponent<NavMeshAgent>();
            if (GameManager.unitService != null && !isInitSet)
            {
                timer = GameManager.unitService.GetPatrolTimer(); // Ÿ�̸� �ʱ�ȭ
                isInitSet = true;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (GameManager.unitService == null)
            {
                return TaskStatus.Failure;
            }

            timer += Time.deltaTime;

            if (timer >= GameManager.unitService.GetPatrolTimer())
            {
                Vector3 newPos = RandomNavSphere(transform.position, GameManager.unitService.GetPatrolRadius(), -1);
                agent.SetDestination(newPos);
                timer = 0; // Ÿ�̸� �缳��

                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
        

        /// <summary>
        /// ���� �̵��� ��ġ
        /// </summary>
        private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;

            randDirection += origin;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

            return navHit.position;
        }
    }
}
