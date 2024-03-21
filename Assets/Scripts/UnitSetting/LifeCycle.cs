using Definition;
using System.Collections;
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

    private NavMeshAgent agent;

    public ILifeCycleService iLifeCycleService;

    private void Awake()
    {
        iLifeCycleService = this.transform.parent.GetComponent<ILifeCycleService>();
    
        StartLifeCycleSubscribe(iLifeCycleService.GetLifeCycleService());
        DeadCycleSubscribe(iLifeCycleService.GetLifeCycleService());  
    }

    public void StartLifeCycleSubscribe(LifeCycleService publisher)
    {
        publisher.startLifeCycle += () =>
        {
            agent = GetComponent<NavMeshAgent>();
            if (agent != null && iLifeCycleService.GetCurrAge().ToString() != agent.gameObject.name) return;

            InitData();

            StartCoroutine(DecreaseStatus());
            StartCoroutine(SetHealthCondition());
            StartCoroutine(SetHappinessCondition());
            StartCoroutine(SetGrowth());
        };
    }
    public void DeadCycleSubscribe(LifeCycleService publisher)
    {
        publisher.deadCycle += () =>
        {
            if (iLifeCycleService == null || agent == null) return;
            if (iLifeCycleService.GetCurrAge().ToString() != agent.gameObject.name) return;
            if (iLifeCycleService.GetCurrAge() != AgeType.Dead) return;
            StartCoroutine(Dead());
        };
    }
    /// <summary>
    /// 데이터 전체 초기화
    /// </summary>
    private void InitData()
    {
        if (iLifeCycleService.GetCurrAge() == AgeType.Old)
            agent.speed = 1f;
        else
            agent.speed = 3f;
        ///감소 수치 설정
        getHungry = UnityEngine.Random.Range(0.1f, 1f);
        getThirst = UnityEngine.Random.Range(0.5f, 1.2f);
        getHappiness = 5f;

        //전체 데이터 초기화
        iLifeCycleService.GetUnitService().InitData();
    }


    /// <summary>
    /// 현재 연령별 노화 속도
    /// </summary>
    private float GetAgeFigure()
    {
        switch (iLifeCycleService.GetCurrAge())
        {
            case AgeType.Child:
                return 10f;
            case AgeType.Adult:
                return 1f;
            case AgeType.Old:
                return 1f;
        }
        return 0;
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
        while (true)
        {
            if (iLifeCycleService.GetCurrAge() != AgeType.Egg)
            {
                if (iLifeCycleService.GetUnitService() != null)
                {
                    yield return new WaitForSeconds(1f);
                    iLifeCycleService.GetUnitService().SetAddAgeFigure(GetAgeFigure());

                    if (iLifeCycleService.GetUnitService().GetAgeFigure() >=
                        iLifeCycleService.GetUnitService().GetMaxAgeFigure())
                    {
                        iLifeCycleService.GetUnitService().SetGrowthEventPosi(this.gameObject.transform.position);
                        if (iLifeCycleService.GetCurrAge() == AgeType.Old)
                            iLifeCycleService.Dead();
                        else
                            iLifeCycleService.GrowUp();
                        break;
                    }
                }
            }
            else
                this.transform.position =
                        iLifeCycleService.GetUnitService().GetGrowthEventPosi();
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
    /// 사망
    /// </summary>
    /// <returns></returns>
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    /*

    #region Interface

    public void StartLifeCycle()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null && iLifeCycleService.GetCurrAge().ToString() != agent.gameObject.name) return;

        InitData();

        StartCoroutine(DecreaseStatus());
        StartCoroutine(SetHealthCondition());
        StartCoroutine(SetHappinessCondition());
        StartCoroutine(SetGrowth());
    }
    #endregion
    */
}
