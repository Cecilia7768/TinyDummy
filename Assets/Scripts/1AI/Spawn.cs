using BehaviorDesigner.Runtime.Tasks;
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
            agent.isStopped = false; 
        }

        public override TaskStatus OnUpdate()
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
            {
                unitService.InitSetHappiness();
                EnvironmentManager.Instance.SpawnEGG();
                return TaskStatus.Success;
            }

            if (agent.velocity.sqrMagnitude == 0f)
            {
                agent.SetDestination(EnvironmentManager.Instance.nestPosi.transform.position);
            }

            return TaskStatus.Running;
        }
    }
}
