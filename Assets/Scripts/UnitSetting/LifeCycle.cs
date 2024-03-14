using Definition;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LifeCycle : MonoBehaviour 
{   
    [SerializeField]
    [Header("초당 허기/갈증 변화 수치")]
    private float getHungry; //초당 배고파지는 수치
    [SerializeField]
    private float getThirst; //초당 목말라지는 수치
    [Header("틱당 행복도 증가 수치")]
    [SerializeField]
    private float getHappiness;
    [Space(3)]
    [Header("틱당 나이 증가 수치")]
    [SerializeField]
    private float getAgeFigure;

    private NavMeshAgent agent;

    public ILifeCycleService iLifeCycleService;

    private void Awake()
    {
        iLifeCycleService = this.transform.parent.GetComponent<ILifeCycleService>();
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        getHungry = UnityEngine.Random.Range(0.1f, 1f);
        getThirst = UnityEngine.Random.Range(0.5f, 1.2f);
        getAgeFigure = 1f;
        StartCoroutine(DecreaseStatus());

        StartCoroutine(SetHealthCondition());
        StartCoroutine(SetHappinessCondition());
        StartCoroutine(MakeBabyJJack());
        StartCoroutine(SetGrowth());
    }


    /// <summary>
    /// 현재 연령별 노화 속도
    /// </summary>
    private void GetAgeFigure()
    {
        switch (iLifeCycleService.GetCurrAge())
        {
            case AgeType.Egg:
                break;
            case AgeType.Child:
                break;
            case AgeType.Adult:
                break;
            case AgeType.Old:
                break;
        }
    }

    /// <summary>
    /// 시간에 따른 허기짐, 갈증
    /// </summary>
    /// <returns></returns>
    IEnumerator DecreaseStatus()
    {
        while (true)
        {
            if (iLifeCycleService.GetUnitService() != null)
            {
                iLifeCycleService.GetUnitService().SetHungry(-getHungry);
                iLifeCycleService.GetUnitService().SetThirst(-getThirst);
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }

    /// <summary>
    /// 연령에 따른 성장 진행
    /// </summary>
    /// <returns></returns>
    IEnumerator SetGrowth()
    {
        while(true)
        {
            if (iLifeCycleService.GetUnitService() != null)
            {
                iLifeCycleService.GetUnitService().SetAgeFigure(getAgeFigure);
                if(iLifeCycleService.GetUnitService().GetAgeFigure() ==
                    iLifeCycleService.GetUnitService().GetMaxAgeFigure())
                {
                   // iLifeCycleService.SetCurrAge(1);
                }
                yield return new WaitForSeconds(1f);
            }
            yield return null;
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
            if (iLifeCycleService.GetUnitService() == null) yield return null;
            if (iLifeCycleService.GetUnitService().GetHungry() >= 80 && iLifeCycleService.GetUnitService().GetThirst() >= 80)
                iLifeCycleService.GetUnitService().SetHealth(1);
            else if (iLifeCycleService.GetUnitService().GetHungry() <= 0 || iLifeCycleService.GetUnitService().GetThirst() <= 0)
                iLifeCycleService.GetUnitService().SetHealth(-1);

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
            if (iLifeCycleService.GetUnitService() == null) yield return null;
            if (iLifeCycleService.GetUnitService().GetHealth() >= 80)
            {
                iLifeCycleService.GetUnitService().SetHappiness(getHappiness);
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
            if (iLifeCycleService.GetUnitService() == null) yield return null;
            yield return null;

            if (iLifeCycleService.GetUnitService().GetHappiness() >= 100)
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
