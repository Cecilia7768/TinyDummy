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
        public float stationaryThreshold = 0.1f; // ���� �������� �ʴ� ������ ���ֵǴ� �ִ� �Ÿ�
        public float resetPathTime = 2f; // ��θ� �ٽ� ����ϱ� ���� �����־�� �ϴ� �ð�

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();
            // �����ο� ��� -> ��� �ּ�
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

            // ���� ��ġ�� ���� ��ġ ������ �Ÿ��� �ſ� �۴ٸ�, ������Ʈ�� ���� �������� �ʰ� �ִٰ� ����
            if (Vector3.Distance(previousPosition, transform.position) <= stationaryThreshold)
            {
                stationaryTimer += Time.deltaTime;
                // ���� �ð� ���� ���� �������� �ʾҴٸ�, ��θ� �ٽ� ���
                if (stationaryTimer >= resetPathTime)
                {
                   // �����ο� ��� -> ��� �ּ�
                   // agent.SetDestination(EnvironmentManager.Instance.nestPosi.transform.position);
                    stationaryTimer = 0f;
                    Debug.LogError("Spawn ����. ��ġ ������");
                }
            }
            else
            {
                // ��ġ�� ���ߴٸ� Ÿ�̸ӿ� ���� ��ġ�� ����
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