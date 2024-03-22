using Definition;
using System;
using UnityEngine;
using UnityEngine.AI;

public class ShapeCycle : MonoBehaviour
{
    private ILifeCycleService iLifeCycleService;

    private void Start()
    {
        iLifeCycleService = this.GetComponent<ILifeCycleService>();

        SetGrowthEventSubscribe(iLifeCycleService.GetLifeCycleService());
        ActiveAgePrefab();
    }
    
    public void SetGrowthEventSubscribe(LifeCycleService publisher)
    {
        publisher.setGrowthEvent += ActiveAgePrefab;
    }

    /// <summary>
    /// ���� ���¿� ���� ������ Ȱ/��Ȱ��ȭ
    /// </summary>
    /// <returns></returns>
    private void ActiveAgePrefab()
    {
        if (iLifeCycleService.GetStatePrefabList().Count == 0) return;

        for (int i = 0; i < iLifeCycleService.GetStatePrefabList().Count; i++)
        {
            iLifeCycleService.GetStatePrefabList()[i].SetActive(false);
        }

        var target = iLifeCycleService.GetStatePrefabList()[(int)iLifeCycleService.GetCurrAge()];
        target?.SetActive(true);
        target.gameObject.transform.position = iLifeCycleService.GetUnitService().GetGrowthEventPosi();
      
        if (iLifeCycleService.GetUnitService().GetEggGrade() == EggGradeType.Special)
        {
            var doubleScale = target.transform.localScale * 2;
            target.transform.localScale = doubleScale;
        }
    }
}
