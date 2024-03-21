using Definition;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HatchCycle : MonoBehaviour
{
    [SerializeField]
    private Slider growBar;

    private const float growthTime = 5f; // 부화까지 걸리는 시간
    private float currGrowthTime = 0f; // 현재 성장 시간

    private ILifeCycleService iLifeCycleService;
    private void Start()
    {
        iLifeCycleService = this.transform.parent.GetComponent<ILifeCycleService>();

        this.transform.position = new Vector3(EnvironmentManager.Instance.nestPosi.position.x
                , 1f, EnvironmentManager.Instance.nestPosi.position.z);
        iLifeCycleService.GetUnitService().SetGrowthEventPosi(this.gameObject.transform.position);
    }
    private void Update()
    {
        if (iLifeCycleService != null)
        {
            if (currGrowthTime < growthTime)
            {
                currGrowthTime += Time.deltaTime;
                if (iLifeCycleService.GetCurrAge() == AgeType.Egg)
                    growBar.value = currGrowthTime / growthTime;
            }
            else
            {
                growBar.value = 1;
                iLifeCycleService.GrowUp();
            }
        }
    }
}
