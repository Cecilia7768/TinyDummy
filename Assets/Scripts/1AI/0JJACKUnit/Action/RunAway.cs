using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class RunAway : Action
    {
        private IUnitService unitService;
        private NavMeshAgent agent;

        private float speed = 5f; // �ʱ� �ӵ�
        private float deceleration = 1f; // �ʴ� ���ӷ�
        private float totalTime = 0f; // �� ��� �ð�
        private Vector3 direction; // �̵� ����

        public override void OnStart()
        {
            unitService = this.transform.parent.GetComponent<IUnitService>();
            agent = this.GetComponent<NavMeshAgent>();
            direction = (transform.position - unitService.GetEnemyObj().transform.position).normalized;
        }

        public override TaskStatus OnUpdate()
        {
            if (unitService.GetEnemyObj() == null)
            {
                return TaskStatus.Success;
            }
            else
            {
                // ����� �ݴ������ �ٶ󺸵��� ����
                Vector3 lookDirection = unitService.GetEnemyObj().transform.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(-lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

                agent.isStopped = true;

                totalTime += Time.deltaTime;

                if (totalTime > 1f && totalTime <= 3f)
                {
                    speed -= deceleration * Time.deltaTime;
                }

                if (speed < 3f)
                {
                    speed = 3f;
                }

                transform.position += direction * speed * Time.deltaTime;

                agent.isStopped = false;
                return TaskStatus.Running;
            }
        }
    }
}
