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

                // ������Ʈ�� Ÿ�� ������Ʈ ������ �Ÿ��� ���
                float distanceToTarget = Vector3.Distance(agent.transform.position, CanSeeObject.targetObject.Value.transform.position);

                // �Ÿ��� 1 �����̸� Ÿ�� ������Ʈ�� ��Ȱ��ȭ
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
