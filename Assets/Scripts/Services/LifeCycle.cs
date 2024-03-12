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
    [Header("초당 허기/갈증 변화 수치")]
    private float getHungry; //초당 배고파지는 수치
    [SerializeField]
    private float getThirst; //초당 목말라지는 수치
    [Header("틱당 행복도 증가 수치")]
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
    /// 시간에 따른 허기짐, 갈증
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
    /// 건강 수치
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
    /// 행복 수치
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
    /// 산란
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

                Debug.Log("도착했습니다!");
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
