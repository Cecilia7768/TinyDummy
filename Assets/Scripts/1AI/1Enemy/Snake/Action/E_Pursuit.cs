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
        private const float checkDuration = 2f; 
        private const float minDistanceChange = 1f;

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
            if (enemyService.GetTarget() == null)
            {
                return TaskStatus.Failure;
            }

            if ((Time.time - lastPathCheckTime >= checkDuration) &&
                (Vector3.Distance(transform.position, lastPosition) <= minDistanceChange))
            {
                agent.SetDestination(enemyService.GetTarget().transform.position);
                lastPathCheckTime = Time.time;
            }

            lastPosition = transform.position;

            targetPosi = new Vector3(enemyService.GetTarget().transform.position.x,
                this.transform.position.y, enemyService.GetTarget().transform.position.z);

            transform.LookAt(targetPosi);

            if (!enemyService.GetIsCanEat())
            {
                agent.isStopped = false;
                agent.SetDestination(enemyService.GetTarget().transform.position);
                return TaskStatus.Running;
            }
            else
            {
                agent.isStopped = true;
                enemyService.GetTarget().gameObject.transform.parent.GetComponent<ILifeCycleService>().GetUnitService().SetIsAttacked(true);
                Debug.LogError("���� //");
                return TaskStatus.Success;
            }
        }
    }
}