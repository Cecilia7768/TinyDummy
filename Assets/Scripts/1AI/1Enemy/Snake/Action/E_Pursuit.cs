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
            GameObject target = enemyService.GetTarget();

            if (target == null)
            {
                return TaskStatus.Failure;
            }
            else
            {
                float currentDistance = Vector3.Distance(target.transform.position, lastTargetPosition);

                // 타겟의 위치가 실제로 변경되었는지 확인
                if (currentDistance >= 0.1f)
                {
                    // 타겟의 위치가 변경되었다면, 시간과 위치 업데이트
                    lastTargetPositionUpdateTime = Time.time;
                    lastTargetPosition = target.transform.position;
                }
                else if (Time.time - lastTargetPositionUpdateTime >= 2f)
                {
                    // 타겟의 위치가 2초 동안 변경되지 않았다면, 재설정 요청
                    Debug.LogError("타겟 재설정 요청");
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