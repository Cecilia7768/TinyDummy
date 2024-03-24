using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangeTrigger : MonoBehaviour
{
    //일단 적이 단일개체라 가정하고 작성됨
    //나중에 다중의 적으로 바뀌면 ViewRangeTrigger 와 같은 로직으로 변경

    private IUnitService unitService;

    // 도망 칠 때의 추가 거리
    public float fleeDistance = 10f;

    private void Awake()
    {
        unitService = this.transform.parent.parent.GetComponent<IUnitService>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "BeFoundRange")
        {
            unitService.SetIsFoundEnemy(true);
            unitService.SetEnemyObj(other.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "BeFoundRange")
        {
            Debug.LogError("도망완료");
            unitService.SetIsFoundEnemy(false);
            unitService.SetEnemyObj(null);
        }
    }
}
