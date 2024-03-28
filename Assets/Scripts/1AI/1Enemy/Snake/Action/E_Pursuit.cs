using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.AI
{
    public class E_Pursuit : Action
    {
        private NavMeshAgent agent;
        private IEnemyService enemyService;

        //��ġ�� ������ ���� �� ��� ����
        private float lastPathCheckTime;
        private Vector3 lastPosition;
        private const float checkDuration = 1f;
        private const float minDistanceChange = 1f;

        private Vector3 lastTargetPosition; // Ÿ���� ������ ��ġ�� ����
        private float lastTargetPositionUpdateTime; // Ÿ�� ��ġ�� ���������� ������Ʈ�� �ð�

        //�ǽð����� �ٶ󺸱�
        private Vector3 targetPosi;
        public override void OnAwake()
        {
            agent = GetComponent<NavMeshAgent>();
            enemyService = GetComponent<EnemyService>();
            lastPosition = transform.position;
        }

        public override TaskStatus OnUpdate()
        {
            GameObject target = enemyService.GetTarget();

            if (target == null)
            {
                return TaskStatus.Failure;
            }
            else
            {
                float currentDistance = Vector3.Distance(target.transform.position, lastTargetPosition);

                // Ÿ���� ��ġ�� ������ ����Ǿ����� Ȯ��
                if (currentDistance >= 0.1f)
                {
                    // Ÿ���� ��ġ�� ����Ǿ��ٸ�, �ð��� ��ġ ������Ʈ
                    lastTargetPositionUpdateTime = Time.time;
                    lastTargetPosition = target.transform.position;
                }
                else if (Time.time - lastTargetPositionUpdateTime >= 2f)
                {
                    // Ÿ���� ��ġ�� 2�� ���� ������� �ʾҴٸ�, �缳�� ��û
                    Debug.LogError("Ÿ�� �缳�� ��û");
                    ViewRangeTrigger.resetTarget = true; 
                    lastTargetPositionUpdateTime = Time.time; 
                }

                if ((Time.time - lastPathCheckTime >= checkDuration) &&
                    (Vector3.Distance(transform.position, lastPosition) <= minDistanceChange))
                {
                    agent.SetDestination(target.transform.position);
                    lastPathCheckTime = Time.time;
                }

                lastPosition = transform.position;

                targetPosi = new Vector3(target.transform.position.x,
                    this.transform.position.y, target.transform.position.z);

                transform.LookAt(targetPosi);

                if (!enemyService.GetIsCanEat())
                {
                    agent.SetDestination(target.transform.position);
                }
                else
                {
                    target.gameObject.transform.parent.GetComponent<ILifeCycleService>().GetUnitService().SetIsAttacked(true);
                    return TaskStatus.Success;
                }
                return TaskStatus.Running;
            }
        }
    }
}