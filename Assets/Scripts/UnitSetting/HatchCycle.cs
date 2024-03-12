using Definition;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HatchCycle : LifeCycle
{
    [SerializeField]
    private Slider growBar;

    private const float growthTime = 2f; // 부화까지 걸리는 시간
    private float currGrowthTime = 0f; // 현재 성장 시간

    private void OnEnable()
    {
        StartCoroutine(Hatching());
    }

    private void Update()
    {
        if (CurrAge == AgeType.Egg)
        {
            growBar.value = currGrowthTime / growthTime;
        }
    }

    /// <summary>
    /// 부화 진행
    /// </summary>
    /// <returns></returns>
    IEnumerator Hatching()
    {
        while (currGrowthTime < growthTime)
        {
            currGrowthTime += Time.deltaTime; 
            yield return null; 
        }
        growBar.value = 1;

        CurrAge = AgeType.Adult;
    }

}
