using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
using Unity.VisualScripting;

public class LifeCycle : MonoBehaviour
{
    [SerializeField]
    [Header("초당 허기/갈증 변화 수치")]
    private float getHungry; //초당 배고파지는 수치
    [SerializeField]
    private float getThirst; //초당 목말라지는 수치

    private IUnitService unitService;

    private void Start()
    {
        getHungry = Random.Range(0.1f, 1f);
        getThirst = Random.Range(0.5f, 1.2f);
        StartCoroutine(DecreaseStatus());
    }

    /// <summary>
    /// 시간에 따른 허기짐, 갈증
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
