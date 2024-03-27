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
    [Header("ƽ�� �ູ�� ��/���� ��ġ")]
    [SerializeField]
    private float getHappiness;

    private NavMeshAgent agent;

    public ILifeCycleService iLifeCycleService;

    private void Awake()
    {
        iLifeCycleService = this.transform.parent.GetComponent<ILifeCycleService>();

        StartLifeCycleSubscribe(iLifeCycleService.GetLifeCycleService());
    }

    public void StartLifeCycleSubscribe(LifeCycleService publisher)
    {
        publisher.startLifeCycle += (AgeType _) =>
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

    /// <summary>
    /// ������ ��ü �ʱ�ȭ
    /// </summary>
    private void InitData()
    {
        float num = 1f;
        if (iLifeCycleService.GetUnitService().GetEggGrade() == EggGradeType.Special)
            num = 2f;

        if (iLifeCycleService.GetCurrAge() == AgeType.Old)
            agent.speed = 1f * num;
        else
            agent.speed = 3f * num;
        ///���� ��ġ ����
        getHungry = UnityEngine.Random.Range(0.1f, 3f);
        getThirst = UnityEngine.Random.Range(0.5f, 3.2f);
        getHappiness = 5f;

        //��ü ������ �ʱ�ȭ
        iLifeCycleService.GetUnitService().InitData();
    }


    /// <summary>
    /// ���� ���ɺ� ��ȭ �ӵ�
    /// </summary>
    private float GetAgeFigure()
    {
        switch (iLifeCycleService.GetCurrAge())
        {
            case AgeType.Child:
                return 2f;
            case AgeType.Adult:
                return 1f;
            case AgeType.Old:
                return 4f;
        }
        return 0;
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
    /// �ǰ� ��ġ
    /// </summary>
    /// <returns></returns>
    IEnumerator SetHealthCondition()
    {
        float hungry;
        float thirst;
        float health;
        float happiness;
        while (true)
        {
            hungry = iLifeCycleService.GetUnitService().GetHungry();
            thirst = iLifeCycleService.GetUnitService().GetThirst();
            health = iLifeCycleService.GetUnitService().GetHealth();
            happiness = iLifeCycleService.GetUnitService().GetHappiness();

            if (iLifeCycleService.GetUnitService() == null) yield return null;
            if (hungry >= 80 && thirst >= 80)
                iLifeCycleService.GetUnitService().SetHealth(1);
            else if (hungry <= 0 || thirst <= 0)
                iLifeCycleService.GetUnitService().SetHealth(-1);

            //ü�� 0�̸� ���
            if (iLifeCycleService.GetUnitService().GetHealth() <= 0)
                iLifeCycleService.Dead(false);
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
            else if (iLifeCycleService.GetUnitService().GetHealth() <= 0)
                iLifeCycleService.GetUnitService().SetHappiness(-getHappiness);

            yield return new WaitForSeconds(2f);
        }
    }
}
