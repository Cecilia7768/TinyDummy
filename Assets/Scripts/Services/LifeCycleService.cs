using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycleService : MonoBehaviour, ILifeCycleService
{
    public LifeCycleStatus lifeCycleStatus;

    public delegate void EventHandler(AgeType beforeAge = AgeType.None);
    //�����Ҷ����� �߻��Ǵ� �̺�Ʈ
    public event EventHandler setGrowthEvent;
    //�����Ҷ� ����������Ŭ ����
    public event EventHandler startLifeCycle;

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
    /// ��� (��ȭ �Ǵ� ��ɴ���)
    /// </summary>
    /// <returns></returns>
    public void Dead(bool isOld = true)
    {
        AgeType beforeAge = lifeCycleStatus.CurrAge;
        if (isOld)
        {
            lifeCycleStatus.CurrAge++;
            JjackStandard.OldCount--;
        }
        else //��ɴ��ؼ� �����Ÿ�
        {
            switch (lifeCycleStatus.CurrAge)
            {
                case AgeType.Egg:
                    JjackStandard.EggCount--;
                    break;
                case AgeType.Child:
                    JjackStandard.ChildCount--;
                    break;
                case AgeType.Adult:
                    JjackStandard.AdultCount--;
                    break;
                case AgeType.Old:
                    JjackStandard.OldCount--;
                    break;  
            }
            lifeCycleStatus.unitService.SetGrowthEventPosi(lifeCycleStatus.statePrefabList[(int)lifeCycleStatus.CurrAge].transform.position);
            lifeCycleStatus.CurrAge = AgeType.Dead;
        }
        setGrowthEvent?.Invoke(beforeAge);

        JjackStandard.DeadCount++;

        if (lifeCycleStatus.unitService.GetGender() == GenderType.Male)
            JjackStandard.MaleCount--;
        else
            JjackStandard.FemaleCount--;

        if (lifeCycleStatus.UnitService.GetEggGrade() == EggGradeType.Special)
            JjackStandard.BossCount--;

        JjackStandard.TotalCount--;
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
