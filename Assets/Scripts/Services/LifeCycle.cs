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

    //private UnitService unitService;

    private void Start()
    {
        StartCoroutine(DecreaseStatus());
    }

    /// <summary>
    /// �ð��� ���� �����, ����
    /// </summary>
    /// <returns></returns>
    IEnumerator DecreaseStatus()
    {
        while (true)
        {
            if(GameManager.unitService != null)
            {
                GameManager.unitService.SetHungry(-getHungry);
                GameManager.unitService.SetThirst(-getThirst);
                yield return new WaitForSeconds(1f);
            }
        }
    }



}
