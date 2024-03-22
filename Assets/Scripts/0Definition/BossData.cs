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
        //우두머리 2마리 생성 가능
        BossAbilityList.Add(num, () => { JjackStandard.BossMaxCount = 2; Debug.LogError("우두머리 2마리 생성 가능"); });
        //쌍생 산란 확률 20% -> 40%
        BossAbilityList.Add(++num, () => { JjackStandard.ProbabilityDoubleEgg = 80f; Debug.LogError("쌍생 산란 확률 20% -> 80%"); });

        //우두머리가 사망할때까지 전체 유닛 속도 1.5배 증가
        //로직 검증 필요
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

        //    Debug.LogError("우두머리가 사망할때까지 전체 유닛 속도 1.5배 증가");
        //});
    }
}