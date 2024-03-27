using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycleService : MonoBehaviour, ILifeCycleService
{
    public LifeCycleStatus lifeCycleStatus;

    public delegate void EventHandler(AgeType beforeAge = AgeType.None);
    //성장할때마다 발생되는 이벤트
    public event EventHandler setGrowthEvent;
    //성장할때 라이프사이클 시작
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

    /// <summary>
    /// 사망 (노화 또는 사냥당한)
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
        else //사냥당해서 죽은거면
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

    //나중에 유저가 직접 치우게 될 경우 추가
    IEnumerator CorDead()
    {
        yield return new WaitForSeconds(JjackStandard.BodyExtinctionTime);
        Destroy(this.gameObject);
    }
    #endregion
}
