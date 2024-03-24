using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Enemy.AI
{
    public class E_Attack : Action
    {
        private IEnemyService enemyService;
        public override void OnStart()
        {
            base.OnStart();
            enemyService = GetComponent<EnemyService>();
        }

        public override TaskStatus OnUpdate()
        {
            enemyService.SetHuntingProgress(1);
            return TaskStatus.Success;
        }        
    }
}