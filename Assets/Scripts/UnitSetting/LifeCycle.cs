using Definition;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LifeCycle : MonoBehaviour
{
    private IUnitService unitService;
    public IUnitService UnitService
    {
        get { return unitService; }
        set
        {
            unitService = value;
        }
    }

    [SerializeField]
    private AgeType currAge;
    public AgeType CurrAge
    {
        get { return currAge; }
        set
        {
            currAge = value;
            setAgeData?.Invoke();
        }
    }

    [Space(5)]
    [Header("���� ����")]
    [SerializeField]
    private GameObject[] statePrefabList;

    public static event Action setAgeData;

    private bool isSetEvent = false;
    private void OnEnable()
    {
        if (!isSetEvent)
        {
            setAgeData += Init;
            isSetEvent = true;
        }

        CurrAge = AgeType.Egg;
        ActiveAgePrefab();
    }
    private void Init()
    {
        ActiveAgePrefab();

        //���߿� ���ɿ� ���� ��� �ʿ�
        if (currAge == AgeType.Child || currAge == AgeType.Adult || currAge == AgeType.Old)
        {
        }
        else if (currAge == AgeType.Egg)
        {

        }
        else if (currAge == AgeType.Dead)
        {

        }
    }

    /// <summary>
    /// ���� ���¿� ���� ������ Ȱ/��Ȱ��ȭ
    /// </summary>
    /// <returns></returns>
    private void ActiveAgePrefab()
    {
        for (int i = 0; i < statePrefabList.Length; i++)
        {
            statePrefabList[i].SetActive(false);
        }
        statePrefabList[(int)CurrAge].SetActive(true);
    }
}
