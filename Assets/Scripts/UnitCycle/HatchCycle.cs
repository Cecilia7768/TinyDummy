using Definition;
using UnityEngine;
using UnityEngine.UI;
public class HatchCycle : MonoBehaviour
{
    [SerializeField]
    private Slider growBar;

    [SerializeField]
    private Material[] eggMaterials;

    private const float growthTime = 5f; // 부화까지 걸리는 시간
    private float currGrowthTime = 0f; // 현재 성장 시간

    private ILifeCycleService iLifeCycleService;
    private void Awake()
    {
        iLifeCycleService = this.transform.parent.GetComponent<ILifeCycleService>();

        iLifeCycleService.GetUnitService().SetGrowthEventPosi(this.gameObject.transform.position);
        if (iLifeCycleService.GetUnitService().GetEggGrade() == EggGradeType.Special)
        {
            this.transform.localScale = new UnityEngine.Vector3(2, 2.3f, 2);
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
                renderer.material = eggMaterials[(int)EggGradeType.Special];
        }
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
