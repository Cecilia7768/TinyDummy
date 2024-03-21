using Definition;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HatchCycle : MonoBehaviour
{
    [SerializeField]
    private Slider growBar;

    private const float growthTime = 5f; // ��ȭ���� �ɸ��� �ð�
    private float currGrowthTime = 0f; // ���� ���� �ð�

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
