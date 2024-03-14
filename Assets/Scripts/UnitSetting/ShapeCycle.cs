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
    /// 성장 상태에 따른 프리팹 활/비활성화
    /// </summary>
    /// <returns></returns>
    private void ActiveAgePrefab()
    {
        if (iLifeCycleService.GetStatePrefabList().Count == 0) return;

        for (int i = 0; i < iLifeCycleService.GetStatePrefabList().Count; i++)
        {
            iLifeCycleService.GetStatePrefabList()[i].SetActive(false);
        }
            
        Debug.LogError(iLifeCycleService.GetCurrAge().ToString());
        iLifeCycleService.GetStatePrefabList()[(int)iLifeCycleService.GetCurrAge()]?.SetActive(true);
    }


}
