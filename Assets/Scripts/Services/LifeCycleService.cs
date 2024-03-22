using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycleService : MonoBehaviour, ILifeCycleService
{
    public LifeCycleStatus lifeCycleStatus;

    public delegate void EventHandler();
    //�����Ҷ����� �߻��Ǵ� �̺�Ʈ
    public event EventHandler setGrowthEvent;
    //�����Ҷ� ����������Ŭ ����
    public event EventHandler startLifeCycle;

    //���
    public event EventHandler deadCycle;

    private void Awake()
    {
        lifeCycleStatus.CurrAge = AgeType.Egg;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            lifeCycleStatus.statePrefabList.Add(transform.GetChild(i).gameObject);
        }
    }

    #region interface
    public IUnitService GetUnitService()
    {
        if (lifeCycleStatus.UnitService == null)
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

    //�̺�Ʈ �Ҵ� ��� �� ����
    //���߿� ���� �����ؾ� �� ���� ����
    IEnumerator OnSetGrowthEvent()
    {
        while (setGrowthEvent == null)
            yield return null;
        setGrowthEvent?.Invoke();
        while (startLifeCycle == null)
            yield return null;
        startLifeCycle?.Invoke();
    }

    /// <summary>
    /// ���
    /// </summary>
    /// <returns></returns>
    public void Dead()
    {
        lifeCycleStatus.CurrAge++;
        setGrowthEvent?.Invoke();
        //deadCycle?.Invoke();

        JjackStandard.OldCount--;
        JjackStandard.DeadCount++;

        if (lifeCycleStatus.unitService.GetGender() == GenderType.Male)
            JjackStandard.MaleCount--;
        else
            JjackStandard.FemaleCount--;

        if (lifeCycleStatus.UnitService.GetEggGrade() == EggGradeType.Special)
            JjackStandard.BossCount--;

        StartCoroutine(CorDead());
    }

    //���߿� ������ ���� ġ��� �� ��� �߰�
    IEnumerator CorDead()
    {
        yield return new WaitForSeconds(JjackStandard.BodyExtinctionTime);
        Destroy(this.gameObject);
    }
    #endregion
}
