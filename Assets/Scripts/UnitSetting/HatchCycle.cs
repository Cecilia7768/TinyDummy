using Definition;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

        StartCoroutine(SetGrowBarValue());
        StartCoroutine(Hatching());
    }

    IEnumerator SetGrowBarValue()
    {
        while(iLifeCycleService == null) yield return null;
        while(true)
        {
            if (iLifeCycleService.GetCurrAge() == AgeType.Egg)
            {
                growBar.value = currGrowthTime / growthTime;
            }

            yield return null;
        }
    }

    /// <summary>
    /// ��ȭ ����
    /// </summary>
    /// <returns></returns>
    IEnumerator Hatching()
    {
        while (iLifeCycleService == null) yield return null;

        while (currGrowthTime < growthTime)
        {
            currGrowthTime += Time.deltaTime;
            yield return null;
        }
        growBar.value = 1;

        iLifeCycleService.SetCurrAge(AgeType.Child);
    }

}
