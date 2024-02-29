using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
using Unity.VisualScripting;

public class LifeCycle : MonoBehaviour
{
    [SerializeField]
    [Header("�ʴ� ���/���� ��ȭ ��ġ")]
    private float getHungry; //�ʴ� ��������� ��ġ
    [SerializeField]
    private float getThirst; //�ʴ� �񸻶����� ��ġ

    private IUnitService unitService;

    private void Start()
    {
        getHungry = Random.Range(0.1f, 1f);
        getThirst = Random.Range(0.5f, 1.2f);
        StartCoroutine(DecreaseStatus());
    }

    /// <summary>
    /// �ð��� ���� �����, ����
    /// </summary>
    /// <returns></returns>
    IEnumerator DecreaseStatus()
    {
        unitService = this.gameObject.GetComponent<IUnitService>();

        while (unitService == null)
            yield return null;

        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (unitService != null)
            {
                unitService.SetHungry(-getHungry);
                unitService.SetThirst(-getThirst);
            }   
        }
    }

}
