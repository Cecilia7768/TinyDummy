using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
using System;

public class LifeCycleService : MonoBehaviour , ILifeCycleService
{
    public LifeCycleStatus lifeCycleStatus;

    //�����Ҷ����� �߻��Ǵ� �̺�Ʈ
    public static event Action setGrowthEvent;
    //�����ҋ� ����������Ŭ ����
    public static event Action startLifeCycle;

    //���
    public static event Action deadCycle;

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
    public AgeType GetCurrAge() => lifeCycleStatus.CurrAge;
    public List<GameObject> GetStatePrefabList() => lifeCycleStatus.StatePrefabList;

    public void SetUnitService(IUnitService iUnitService) => lifeCycleStatus.UnitService = iUnitService;
    public void GrowUp()
    {
        lifeCycleStatus.CurrAge++;
        setGrowthEvent?.Invoke();
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
