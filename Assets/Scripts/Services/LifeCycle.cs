using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
using Unity.VisualScripting;
using UnityEditor;
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

    private IUnitService unitService;

    private NavMeshAgent agent;

    private void Start()
    {
        getHungry = Random.Range(0.1f, 1f);
        getThirst = Random.Range(0.5f, 1.2f);
        StartCoroutine(DecreaseStatus());

        agent = GetComponent<NavMeshAgent>();

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
        unitService = this.gameObject.GetComponent<IUnitService>();

        while (unitService == null)
            yield return null;

        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (unitService != null)
            {
                unitService.SetHungry(-getHungry);
                unitService.SetThirst(-getThirst);
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
            if (unitService.GetHungry() >= 80 && unitService.GetThirst() >= 80)
                unitService.SetHealth(1);
            else if (unitService.GetHungry() <= 0 || unitService.GetThirst() <= 0)
                unitService.SetHealth(-1);

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
            if (unitService.GetHealth() >= 80)
            {
                unitService.SetHappiness(getHappiness);
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

            if (unitService.GetHappiness() >= 100)
            {
                agent.destination = EnvironmentManager.Instance.nestPosi.transform.position;

                while (!HasReachedDestination())
                {
                    yield return null;
                }

                Debug.Log("�����߽��ϴ�!");
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
