using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.AI
{
    public class IsCanEat : Conditional
    {
        private IEnemyService enemyService;
        public override void OnAwake()
        {
            enemyService = GetComponent<EnemyService>();
        }
        public override TaskStatus OnUpdate()
        {
            if (enemyService != null && enemyService.GetIsCanEat())
            {
                enemyService.SetIsCanEat(false);
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }

    }
}