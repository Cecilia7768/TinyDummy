using Definition;
using System.Collections;
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


        LifeCycleService.startLifeCycle += () =>
        {
            agent = GetComponent<NavMeshAgent>();
            if (agent != null && iLifeCycleService.GetCurrAge().ToString() != agent.gameObject.name) return;

            InitData();

            StartCoroutine(DecreaseStatus());
            StartCoroutine(SetHealthCondition());
            StartCoroutine(SetHappinessCondition());
            StartCoroutine(MakeBabyJJack());
            StartCoroutine(SetGrowth());
        };

        LifeCycleService.deadCycle += () =>
        {
            if(iLifeCycleService == null || agent == null) return;
            if (iLifeCycleService.GetCurrAge().ToString() != agent.gameObject.name) return;
            if (iLifeCycleService.GetCurrAge() != AgeType.Dead) return;
            StartCoroutine(Dead());
        };
    }


    /// <summary>
    /// ������ ��ü �ʱ�ȭ
    /// </summary>
    private void InitData()
    {
        if (iLifeCycleService.GetCurrAge() == AgeType.Old)
            agent.speed = 1f;
        else
            agent.speed = 3f;
        ///���� ��ġ ����
        getHungry = UnityEngine.Random.Range(0.1f, 1f);
        getThirst = UnityEngine.Random.Range(0.5f, 1.2f);
        getHappiness = 5f;
        getAgeFigure = 1f;

        //��ü ������ �ʱ�ȭ
        iLifeCycleService.GetUnitService().InitData();
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
        while (true)
        {
            if (iLifeCycleService.GetCurrAge() != AgeType.Egg)
            {
                if (iLifeCycleService.GetUnitService() != null)
                {
                    yield return new WaitForSeconds(1f);
                    iLifeCycleService.GetUnitService().SetAddAgeFigure(getAgeFigure);

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

    /// <summary>
    /// ���
    /// </summary>
    /// <returns></returns>
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
