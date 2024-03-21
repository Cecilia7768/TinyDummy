using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
using System;

public class LifeCycleService : MonoBehaviour , ILifeCycleService
{
    public LifeCycleStatus lifeCycleStatus;

    public delegate void EventHandler();
    //성장할때마다 발생되는 이벤트
    public event EventHandler setGrowthEvent;
    //성장할때 라이프사이클 시작
    public event EventHandler startLifeCycle;

    //사망
    public event EventHandler deadCycle;

    private void Awake()
    {
        lifeCycleStatus.CurrAge = AgeType.Egg;

        for (int i = 0; i< this.transform.childCount; i++)
        {
            lifeCycleStatus.statePrefabList.Add(transform.GetChild(i).gameObject);
        }
    }

    #region interface
    public IUnitService GetUnitService()
    {
        if(lifeCycleStatus.UnitService == null)
            lifeCycleStatus.UnitService = GetComponent<UnitService>();
        return lifeCycleStatus.UnitService;
    }

    public LifeCycleService GetLifeCycleService() => this;
    public AgeType GetCurrAge() => lifeCycleStatus.CurrAge;
    public List<GameObject> GetStatePrefabList() => lifeCycleStatus.StatePrefabList;

    public void SetUnitService(IUnitService iUnitService) => lifeCycleStatus.UnitService = iUnitService;
    public void GrowUp()
    {
        lifeCycleStatus.CurrAge++;
        StartCoroutine(OnSetGrowthEvent());
    }

    //이벤트 할당 대기 후 실행
    //나중에 로직 수정해야 할 수도 있음
    IEnumerator OnSetGrowthEvent()
    {
        while (setGrowthEvent == null)
            yield return null;
        setGrowthEvent?.Invoke();
        while (startLifeCycle == null)
            yield return null;
        startLifeCycle?.Invoke();
    }
    public void Dead()
    {
        lifeCycleStatus.CurrAge++;
        setGrowthEvent?.Invoke();
        deadCycle?.Invoke();
    }
    #endregion
}
