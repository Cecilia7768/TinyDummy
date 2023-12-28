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

    //private UnitService unitService;

    private void Start()
    {
        StartCoroutine(DecreaseStatus());
    }

    /// <summary>
    /// 시간에 따른 허기짐, 갈증
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
