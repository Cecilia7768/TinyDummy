using Definition;
using System;
using UnityEngine;

public class ShapeCycle : MonoBehaviour
{

    private ILifeCycleService iLifeCycleService;

    private void Start()
    {
        iLifeCycleService = this.GetComponent<ILifeCycleService>();

        LifeCycleService.setGrowthEvent += ActiveAgePrefab;
        ActiveAgePrefab();
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
            
        iLifeCycleService.GetStatePrefabList()[(int)iLifeCycleService.GetCurrAge()]?.SetActive(true);
        iLifeCycleService.GetStatePrefabList()[(int)iLifeCycleService.GetCurrAge()].gameObject.transform.position
            = iLifeCycleService.GetUnitService().GetGrowthEventPosi();
    }


}
