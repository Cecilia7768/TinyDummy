using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangeTrigger : MonoBehaviour
{
    //�ϴ� ���� ���ϰ�ü�� �����ϰ� �ۼ���
    //���߿� ������ ������ �ٲ�� ViewRangeTrigger �� ���� �������� ����

    private IUnitService unitService;

    // ���� ĥ ���� �߰� �Ÿ�
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
            Debug.LogError("�����Ϸ�");
            unitService.SetIsFoundEnemy(false);
            unitService.SetEnemyObj(null);
        }
    }
}
