using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRangeTrigger : MonoBehaviour
{
    private IEnemyService enemyService;

    //���� �ȿ� ���� JJACK List
    private List<GameObject> triggerJJACK = new List<GameObject>();

    private void Start()
    {
        enemyService = transform.parent.GetComponent<EnemyService>();
    }
    void Update()
    {
        if (triggerJJACK.Count == 0)
        {
            enemyService.SetTarget(null);
            return;
        }
        else if (enemyService != null)
            enemyService.SetTarget(FindClosestJJACK());
    }

    /// <summary>
    /// ���� �� ���� ����� JJACK
    /// </summary>
    /// <returns></returns>
    private GameObject FindClosestJJACK()
    {
        GameObject closestJJACK = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject jjack in triggerJJACK)
        {
            if (jjack != null)
            {
                float distance = Vector3.Distance(jjack.transform.position, currentPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestJJACK = jjack;
                }
            }
        }
        return closestJJACK;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("JJACK") && !triggerJJACK.Contains(other.gameObject))
        {
            triggerJJACK.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // ������Ʈ�� �ݶ��̴� ������ ��� ��� ����Ʈ���� ����
        if (other.CompareTag("JJACK") && triggerJJACK.Contains(other.gameObject))
        {
            triggerJJACK.Remove(other.gameObject);
        }
    }
}
