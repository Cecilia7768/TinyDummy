using Definition;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class JjackCycle : LifeCycle
{
    [SerializeField]
    [Header("�ʴ� ���/���� ��ȭ ��ġ")]
    private float getHungry; //�ʴ� ��������� ��ġ
    [SerializeField]
    private float getThirst; //�ʴ� �񸻶����� ��ġ
    [Header("ƽ�� �ູ�� ���� ��ġ")]
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
    /// �ð��� ���� �����, ����
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
    /// �ǰ� ��ġ
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
    /// �ູ ��ġ
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
    /// ���
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
