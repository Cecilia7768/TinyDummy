using Definition;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HatchCycle : LifeCycle
{
    [SerializeField]
    private Slider growBar;

    private const float growthTime = 2f; // ��ȭ���� �ɸ��� �ð�
    private float currGrowthTime = 0f; // ���� ���� �ð�

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
    /// ��ȭ ����
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
