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
    private void ActiveAgePrefab(AgeType beforeAge = AgeType.None)
    {
        if (iLifeCycleService.GetStatePrefabList().Count == 0) retu=n;

        for (int i = 0; i < iLifeCycleService.GetStatePrefabList().Count; i++)
        {
            iLifeCycleService.GetStatePrefabList()[i].SetActive(false);
        }

        ///// ���� Ȱ��ȭ�� ���� ������Ʈ ���� ///// 
        var target = iLifeCycleService.GetStatePrefabList()[(int)iLifeCycleService.GetCurrAge()];
        target?.SetActive(true);
        target.transform.position = iLifeCycleService.GetUnitService().GetGrowthEventPosi();
        switch(beforeAge)
        {
            case AgeType.Egg:
                break;
            case AgeType.Child:
                target.transform.localScale = new Vector3(3, 3, 3);
                break;
            case AgeType.Adult:
                target.transform.localScale = new Vector3(5, 5, 5);
                break;
            case AgeType.Old:
                target.transform.localScale = new Vector3(5, 5, 5);
                break;
        }
        //if ((beforeAge != AgeType.Egg || beforeAge != AgeType.None) &&
        //    iLifeCycleService.GetCurrAge() == AgeType.Dead)
        //{
        //    target.transform.localPosition = new Vector3(target.transform.position.x, 1f,
        //        target.transform.position.z);
        //    target.transform.localRotation = Quaternion.Euler(120, 100, 50);
        //}
        /////////////////////////////////////////////
        
        if (iLifeCycleService.GetUnitService().GetEggGrade() == EggGradeType.Special)
        {
            var doubleScale = target.transform.localScale * 2;
            target.transform.localScale = doubleScale;
        }
    }
}
