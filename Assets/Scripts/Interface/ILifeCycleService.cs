using Definition;
using System.Collections.Generic;
using UnityEngine;

public interface ILifeCycleService
{
    public IUnitService GetUnitService();
    public AgeType GetCurrAge();
    public List<GameObject> GetStatePrefabList();

    public void SetUnitService(IUnitService iUnitService);
    public void GrowUp();
    public void Dead();
}
