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
        private ILifeCycleService lifeCycleService;

        public float speedDeceleration = 1f; // 속도 감소량
        private float timeSinceStartRunning = 0f; // 도망치기 시작한 후 경과 시간

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();
            unitService = this.transform.parent.GetComponent<IUnitService>();
            lifeCycleService = this.transform.parent.GetComponent<ILifeCycleService>();

            if (unitService.GetEnemyObj() != null)
            {
                Vector3 runDirection = (transform.position - unitService.GetEnemyObj().transform.position).normalized;
                SetRunAwaySpeed();
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

            // 1초 이후부터 속도 감소 시작
            if (timeSinceStartRunning > 1f)
            {
                agent.speed = Mathf.Max(3f, agent.speed - speedDeceleration * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, unitService.GetEnemyObj().transform.position) > 10f)
            {
                // 적과의 거리가 10 이상이면 성공 반환
                return TaskStatus.Success;
            }

            // 대상의 반대방향을 바라보기
            Vector3 enemyDirection = unitService.GetEnemyObj().transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, -enemyDirection, float.MaxValue, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            return TaskStatus.Running;
        }

        /// <summary>
        /// 연령별 agent 속도 설정
        /// </summary>
        private void SetRunAwaySpeed()
        {
            switch(lifeCycleService.GetCurrAge())
            {
                case AgeType.Child:
                    agent.speed = 4f;
                    break;
                case AgeType.Adult:
                    agent.speed = 7f;
                    break;
                case AgeType.Old:
                    agent.speed = 3f;
                    break;
            }
        }
    }
}
