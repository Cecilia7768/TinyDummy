using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Enemy.AI
{
    public class E_Attack : Action
    {
        private NavMeshAgent agent;
        private IEnemyService enemyService;
        public override void OnStart()
        {
            base.OnStart();
            agent = GetComponent<NavMeshAgent>();
            enemyService = GetComponent<EnemyService>();
        }

        public override TaskStatus OnUpdate()
        {
            enemyService.SetHuntingProgress(1);
            agent.isStopped = false;
            enemyService.SetTarget(null);
            enemyService.SetIsCanHunt(true);
            return TaskStatus.Success;
        }        
    }
}