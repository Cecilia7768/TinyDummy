using Definition;
using System.Collections.Generic;
using UnityEngine;

public interface ILifeCycleService
{
    ///////////////////////// GET /////////////////////////
    public IUnitService GetUnitService();
    public LifeCycleService GetLifeCycleService();
    public AgeType GetCurrAge();

    public List<GameObject> GetStatePrefabList();

    ///////////////////////// SET /////////////////////////
    public void SetUnitService(IUnitService iUnitService);

    public void GrowUp();
    public void Dead(bool isOld = true);
}
