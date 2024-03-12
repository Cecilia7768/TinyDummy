using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Definition;
using UnityEngine;
using UnityEngine.AI;


namespace AI
{
    public class Spawn : Action
    {
        private IUnitService unitService;

        private NavMeshAgent agent;

        public override void OnStart()
        {
            base.OnStart();
            unitService = this.gameObject.GetComponent<IUnitService>();
            agent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            agent.destination = EnvironmentManager.Instance.nestPosi.transform.position;

            if (!HasReachedDestination())
            {
                return TaskStatus.Running;
            }

            return TaskStatus.Success;
        }

        public override void OnEnd()
        {
            base.OnEnd();
            unitService.InitSetHappiness();
            EnvironmentManager.Instance.SpawnEGG();
        }

        bool HasReachedDestination()
        {
            if (!agent.pathPending) // 경로 계산이 완료되었는지 확인
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


    }
}