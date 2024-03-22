using Definition;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossData : MonoBehaviour
{
    private static Dictionary<int, Action> bossAbilityList = new Dictionary<int, Action>();
    public static Dictionary<int, Action> BossAbilityList { get { return bossAbilityList; } }

    private void Awake()
    {
        GameManager.firstGameStartSetInit += () =>
        {
            bossAbilityList.Clear();
            SetData();
        };
    }

    private void SetData()
    {
        int num = 0;    
        //��θӸ� 2���� ���� ����
        BossAbilityList.Add(num, () => { JjackStandard.BossMaxCount = 2; Debug.LogError("��θӸ� 2���� ���� ����"); });
        //�ֻ� ��� Ȯ�� 20% -> 40%
        BossAbilityList.Add(++num, () => { JjackStandard.ProbabilityDoubleEgg = 80f; Debug.LogError("�ֻ� ��� Ȯ�� 20% -> 80%"); });

        //��θӸ��� ����Ҷ����� ��ü ���� �ӵ� 1.5�� ����
        //���� ���� �ʿ�
        //BossAbilityList.Add(++num, () =>
        //{
        //    LifeCycleService lifeCycleService;
        //    NavMeshAgent agent;

        //    foreach (var data in EnvironmentManager.UnitsDic)
        //    {
        //        lifeCycleService = null; agent = null;

        //        lifeCycleService = data.Value.GetComponent<LifeCycleService>();
        //        agent = lifeCycleService.GetStatePrefabList()[(int)lifeCycleService.GetCurrAge()].
        //        GetComponent<NavMeshAgent>();
        //        if (agent != null) agent.speed *= 1.5f;
        //    }

        //    Debug.LogError("��θӸ��� ����Ҷ����� ��ü ���� �ӵ� 1.5�� ����");
        //});
    }
}