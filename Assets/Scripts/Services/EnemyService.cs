using Definition;
using UnityEngine;
using UnityEngine.AI;

public class EnemyService : MonoBehaviour, IEnemyService
{
    public EnemyStatus enemyStatus;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitData();
    }
    private void InitData()
    {
        enemyStatus = new EnemyStatus();

        enemyStatus.Target = null;

        agent.speed = enemyStatus.MoveSpeed = 5f;
        enemyStatus.AttackPower = 1f;
        enemyStatus.AttackSpeed = 1f;
        enemyStatus.HuntingProgress = 0;
        enemyStatus.MaxHuntingProgress = 3;

        enemyStatus.IsCanEat = false;
        enemyStatus.IsCanHunt = true;

        enemyStatus.PatrolRadius = 70f;
        enemyStatus.PatrolTimer = 1f;
    }

    #region Interface
    //////////////////// GET ////////////////////
    public int GetNum() => enemyStatus.Num;

    public GameObject GetTarget() => enemyStatus.Target;
    //public BoxCollider GetViewRange() => enemyStatus.ViewRange;
    //public BoxCollider GetEatRange() => enemyStatus.EatRange;

    public float GetMoveSpeed() => enemyStatus.MoveSpeed;
    public float GetAttackPower() => enemyStatus.AttackPower;
    public float GetAttackSpeed() => enemyStatus.AttackSpeed;

    public bool GetIsCanEat() => enemyStatus.IsCanEat;
    public bool GetIsCanHunt() => enemyStatus.IsCanHunt;

    public int GetHuntingProgress() => enemyStatus.HuntingProgress;
    public int GetMaxHuntingProgress() => enemyStatus.MaxHuntingProgress;

    public float GetPatrolRadius() => enemyStatus.PatrolRadius;
    public float GetPatrolTimer() => enemyStatus.PatrolTimer;

    //////////////////// SET ////////////////////
    public void SetNum(int num) => enemyStatus.Num = num;

    public void SetTarget(GameObject target) => enemyStatus.Target = target;
    //public void SetViewRange(BoxCollider viewRange) => enemyStatus.ViewRange = viewRange;
    //public void SetEatRange(BoxCollider eatRange) => enemyStatus.ViewRange = eatRange;

    public void SetMoveSpeed(float speed) => enemyStatus.MoveSpeed = speed;
    public void SetAttackPower(float attackPower) => enemyStatus.AttackPower = attackPower;
    public void SetAttackSpeed(float attackSpeed) => enemyStatus.AttackSpeed = attackSpeed;
   
    public bool SetIsCanEat(bool isCanEat) => enemyStatus.IsCanEat = isCanEat;
    public bool SetIsCanHunt(bool isCanHunt) => enemyStatus.IsCanHunt = isCanHunt;

    public void SetHuntingProgress(int huntingProgress)
    {
        enemyStatus.Target.GetComponent<NavMeshAgent>().isStopped = true;
        var jjack = enemyStatus.Target.transform.parent.GetComponent<LifeCycleService>();
        if (jjack != null)
        {
            jjack.GetUnitService().SetIsAttacked(true);
            var data = enemyStatus.Target.transform.parent.GetComponent<ILifeCycleService>();
            data.Dead(false); //사냥으로 인한 죽음
            enemyStatus.HuntingProgress++;
            enemyStatus.Target = null;
            enemyStatus.IsCanHunt = false;
        }
    }
    public void SetMaxHuntingProgress(int maxHuntingProgress) => enemyStatus.MaxHuntingProgress = maxHuntingProgress;

    public void SetPatrolRadius(float patrolRadius) => enemyStatus.PatrolRadius = patrolRadius;
    public void SetPatrolTimer(float patrolTimer) => enemyStatus.PatrolTimer = patrolTimer;
    #endregion
}
