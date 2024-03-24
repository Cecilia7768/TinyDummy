using UnityEngine;

public interface IEnemyService
{
    //////////////////// GET ////////////////////
    public int GetNum();

    public GameObject GetTarget();
    public float GetMoveSpeed();
    public float GetAttackPower();
    public float GetAttackSpeed();

    public bool GetIsCanEat();
    public bool GetIsCanHunt();

    public int GetHuntingProgress();
    public int GetMaxHuntingProgress();

    public float GetPatrolRadius();
    public float GetPatrolTimer();

    //////////////////// SET ////////////////////
    public void SetNum(int num);

    public void SetTarget(GameObject target);
    public void SetMoveSpeed(float speed);
    public void SetAttackPower(float attackPower);
    public void SetAttackSpeed(float attackSpeed);

    public bool SetIsCanEat(bool isCanEat);
    public bool SetIsCanHunt(bool isCanHunt);

    public void SetHuntingProgress(int huntingProgress);
    public void SetMaxHuntingProgress(int maxHuntingProgress);

    public void SetPatrolRadius(float patrolRadius);
    public void SetPatrolTimer(float patrolTimer);
}
