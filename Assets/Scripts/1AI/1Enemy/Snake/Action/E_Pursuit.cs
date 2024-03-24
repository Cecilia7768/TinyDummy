using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.AI
{
    public class E_Pursuit : Action
    {
        private NavMeshAgent agent;
        private IEnemyService enemyService;

        //위치가 변하지 않을 때 경로 재계산
        private float lastPathCheckTime; 
        private Vector3 lastPosition; 
        private const float checkDuration = 1f; 
        private const float minDistanceChange = 1f;

        private Vector3 lastTargetPosition; // 타겟의 마지막 위치를 저장
        private float lastTargetPositionUpdateTime; // 타겟 위치가 마지막으로 업데이트된 시간

        //실시간으로 바라보기
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
            else
            {
                // Debug.LogError(enemyService.GetTarget().transform.position);

                // 타겟의 위치가 1초간 동일한지 확인
                if (Vector3.Distance(enemyService.GetTarget().transform.position, lastTargetPosition) < 0.01f)
                {
                    if (Time.time - lastTargetPositionUpdateTime >= 1f)
                    {
                        Debug.LogError("타겟의 위치가 1초간 동일_ 위치조정");
                        enemyService.SetTarget(enemyService.GetTarget());
                        lastTargetPositionUpdateTime = Time.time; // 시간 업데이트
                    }
                }
                else
                {
                    lastTargetPosition = enemyService.GetTarget().transform.position;
                    lastTargetPositionUpdateTime = Time.time; // 위치 및 시간 업데이트
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
                    //agent.isStopped = false;
                    agent.SetDestination(enemyService.GetTarget().transform.position);
                    return TaskStatus.Running;
                }
                else
                {
                    //agent.isStopped = true;
                    enemyService.GetTarget().gameObject.transform.parent.GetComponent<ILifeCycleService>().GetUnitService().SetIsAttacked(true);
                    Debug.LogError("접촉 //");
                    return TaskStatus.Success;
                }
            }
        }
    }
}