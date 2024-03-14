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
    [Header("�ʴ� ���/���� ��ȭ ��ġ")]
    private float getHungry; //�ʴ� ��������� ��ġ
    [SerializeField]
    private float getThirst; //�ʴ� �񸻶����� ��ġ
    [Header("ƽ�� �ູ�� ���� ��ġ")]
    [SerializeField]
    private float getHappiness;
    [Space(3)]
    [Header("ƽ�� ���� ���� ��ġ")]
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
    /// ���� ���ɺ� ��ȭ �ӵ�
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
    /// �ð��� ���� �����, ����
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
    /// ���ɿ� ���� ���� ����
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
    /// �ǰ� ��ġ
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
    /// �ູ ��ġ 
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
    /// ���
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
        if (!agent.pathPending) // ��� ����� �Ϸ�Ǿ����� Ȯ��
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
