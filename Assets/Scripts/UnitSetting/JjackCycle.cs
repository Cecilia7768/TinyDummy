using Definition;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class JjackCycle : LifeCycle
{
    [SerializeField]
    [Header("초당 허기/갈증 변화 수치")]
    private float getHungry; //초당 배고파지는 수치
    [SerializeField]
    private float getThirst; //초당 목말라지는 수치
    [Header("틱당 행복도 증가 수치")]
    [SerializeField]
    private float getHappiness;

    private NavMeshAgent agent;

    private void OnEnable()
    {
        UnitService = this.GetComponent<IUnitService>();
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        getHungry = UnityEngine.Random.Range(0.1f, 1f);
        getThirst = UnityEngine.Random.Range(0.5f, 1.2f);
        StartCoroutine(DecreaseStatus());

        StartCoroutine(SetHealthCondition());
        StartCoroutine(SetHappinessCondition());
        StartCoroutine(MakeBabyJJack());
    }


    /// <summary>
    /// 시간에 따른 허기짐, 갈증
    /// </summary>
    /// <returns></returns>
    IEnumerator DecreaseStatus()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (UnitService != null)
            {
                UnitService.SetHungry(-getHungry);
                UnitService.SetThirst(-getThirst);
            }
        }
    }


    /// <summary>
    /// 건강 수치
    /// </summary>
    /// <returns></returns>
    IEnumerator SetHealthCondition()
    {
        while (true)
        {
            if (UnitService.GetHungry() >= 80 && UnitService.GetThirst() >= 80)
                UnitService.SetHealth(1);
            else if (UnitService.GetHungry() <= 0 || UnitService.GetThirst() <= 0)
                UnitService.SetHealth(-1);

            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// 행복 수치
    /// </summary>
    /// <returns></returns>
    IEnumerator SetHappinessCondition()
    {
        while (true)
        {
            if (UnitService.GetHealth() >= 80)
            {
                UnitService.SetHappiness(getHappiness);
            }

            yield return new WaitForSeconds(2f);
        }
    }

    /// <summary>
    /// 산란
    /// </summary>
    /// <returns></returns>
    IEnumerator MakeBabyJJack()
    {
        while (true)
        {
            yield return null;

            if (UnitService.GetHappiness() >= 100)
            {
                agent.destination = EnvironmentManager.Instance.nestPosi.transform.position;

                while (!HasReachedDestination())
                {
                    yield return null;
                }
                break;
            }
        }
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
