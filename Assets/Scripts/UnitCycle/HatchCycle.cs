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
    private void Start()
    {
        iLifeCycleService = this.transform.parent.GetComponent<ILifeCycleService>();

        //this.transform.position = new UnityEngine.Vector3(EnvironmentManager.Instance.nestPosi.position.x
        //        , 2f, EnvironmentManager.Instance.nestPosi.position.z);

        //this.transform.position = RandomPositionOnNest();

        if (this.transform.position.y < 0)
            this.transform.position = new UnityEngine.Vector3(this.transform.position.x, 1f,
                this.transform.position.z);

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

    /// <summary>
    /// 둥지 내 산란할 위치
    /// </summary>
    /// <returns></returns>
    //private UnityEngine.Vector3 RandomPositionOnNest()
    //{
    //    Renderer planeRenderer = EnvironmentManager.Instance.spawnArea;
    //    UnityEngine.Vector3 planeSize = planeRenderer.bounds.size;

    //    float x = Random.Range(-planeSize.x / 2, planeSize.x / 2);
    //    float z = Random.Range(-planeSize.z / 2, planeSize.z / 2);
    //    UnityEngine.Vector3 position = EnvironmentManager.Instance.spawnArea.transform.position + new UnityEngine.Vector3(x, 1, z);

    //    return position;
    //}
}
