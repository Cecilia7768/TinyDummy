using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRangeTrigger : MonoBehaviour
{
    private IEnemyService enemyService;

    //���� �ȿ� ���� JJACK List
    private List<GameObject> triggerJJACK = new List<GameObject>();
    public static bool jjackReFind = false;

    //Ÿ�� �缳��
    public static bool resetTarget = false;
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

        if(resetTarget)
        {
            resetTarget = false;
            enemyService.SetTarget(ResetTarget());
            Debug.LogError("Ÿ�� ���� �Ϸ�");
        }
    }

    private GameObject ResetTarget()
    {
        List<GameObject> allJJACKs = new List<GameObject>(triggerJJACK); 
        Vector3 currentPosition = transform.position;
        allJJACKs.Sort((a, b) =>
        {
            if (a == null || b == null) return 0;
            float distanceA = Vector3.Distance(a.transform.position, currentPosition);
            float distanceB = Vector3.Distance(b.transform.position, currentPosition);
            return distanceA.CompareTo(distanceB);
        });

        if (allJJACKs.Count > 1)
        {
            GameObject secondClosestJJACK = allJJACKs[1]; 
            enemyService.SetIsCanHunt(secondClosestJJACK != null);
            return secondClosestJJACK;
        }
        else
        {
            enemyService.SetIsCanHunt(false);
            return null;
        }
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
