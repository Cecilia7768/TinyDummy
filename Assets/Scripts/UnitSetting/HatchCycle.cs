using Definition;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HatchCycle : MonoBehaviour
{
    [SerializeField]
    private Slider growBar;

    private const float growthTime = 2f; // ��ȭ���� �ɸ��� �ð�
    private float currGrowthTime = 0f; // ���� ���� �ð�

    private ILifeCycleService iLifeCycleService;
    private void Start()
    {
        iLifeCycleService = this.transform.parent.GetComponent<ILifeCycleService>();
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
