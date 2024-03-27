using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRangeTrigger : MonoBehaviour
{
    private IEnemyService enemyService;

    //���� �ȿ� ���� JJACK List
    private List<GameObject> triggerJJACK = new List<GameObject>();
    public static bool jjackReFind = false; 

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

        //���� ����� ������
        enemyService.SetIsCanHunt(closestJJACK != null);
        return closestJJACK;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.CompareTag("JJACK") && !triggerJJACK.Contains(other.transform.parent.gameObject))
        {
            triggerJJACK.Add(other.transform.parent.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // ������Ʈ�� �ݶ��̴� ������ ��� ��� ����Ʈ���� ����
        if (other.transform.parent.CompareTag("JJACK") && triggerJJACK.Contains(other.transform.parent.gameObject))
        {
            triggerJJACK.Remove(other.transform.parent.gameObject);
        }
    }
}
