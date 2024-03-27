using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class RunAway : Action
    {
        private NavMeshAgent agent;
        private IUnitService unitService;

        public float initialSpeed = 5f; // ����ĥ ���� �ʱ� �ӵ�
        public float speedDeceleration = 1f; // �ӵ� ���ҷ�
        private float timeSinceStartRunning = 0f; // ����ġ�� ������ �� ��� �ð�

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();
            unitService = this.transform.parent.GetComponent<IUnitService>();

            if (unitService.GetEnemyObj() != null)
            {
                Vector3 runDirection = (transform.position - unitService.GetEnemyObj().transform.position).normalized;
                agent.speed = initialSpeed;
                agent.destination = transform.position + runDirection * agent.speed;
                timeSinceStartRunning = 0f;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (unitService.GetEnemyObj() == null)
            {
                return TaskStatus.Success;
            }

            timeSinceStartRunning += Time.deltaTime;

            // 1�� ���ĺ��� �ӵ� ���� ����
            if (timeSinceStartRunning > 1f)
            {
                agent.speed = Mathf.Max(3f, agent.speed - speedDeceleration * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, unitService.GetEnemyObj().transform.position) > 10f)
            {
                // ������ �Ÿ��� 10 �̻��̸� ���� ��ȯ
                return TaskStatus.Success;
            }

            // ����� �ݴ������ �ٶ󺸱�
            Vector3 enemyDirection = unitService.GetEnemyObj().transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, -enemyDirection, float.MaxValue, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            return TaskStatus.Running;
        }
    }
}
