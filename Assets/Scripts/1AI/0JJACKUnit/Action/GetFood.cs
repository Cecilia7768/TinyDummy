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
        private float lastPathCheckTime; // ���������� ��θ� Ȯ���� �ð�
        private Vector3 lastPosition; // ���������� ��ϵ� ��ġ
        private const float checkDuration = 2f; // ��ġ ������ Ȯ���� �ð� ���� (��)
        private const float minDistanceChange = 1f; // "���� ��ġ"�� �Ǵ��� �ּ� �Ÿ� ��ȭ

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
                // ���� ��ġ�� ������ ��ġ�� �Ÿ��� �ſ� �۰�, ������ �ð��� ����ߴ��� Ȯ��
                if ((Time.time - lastPathCheckTime >= checkDuration) &&
                    (Vector3.Distance(transform.position, lastPosition) <= minDistanceChange))
                {
                    // ��ġ�� ���� ������ �ʾҴٸ�, �ٽ� �������� ����
                    agent.SetDestination(CanSeeObject.targetObject.Value.transform.position);
                    lastPathCheckTime = Time.time;
                }

                // ���� ��ġ ������Ʈ
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
