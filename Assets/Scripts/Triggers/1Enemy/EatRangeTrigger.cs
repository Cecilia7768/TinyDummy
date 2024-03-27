using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatRangeTrigger : MonoBehaviour
{
    private IEnemyService enemyService;
    private void Start()
    {
        enemyService = transform.parent.GetComponent<EnemyService>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == enemyService.GetTarget())
        {
            enemyService.SetIsCanEat(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == enemyService.GetTarget())
        {
            enemyService.SetIsCanEat(true);
        }
    }
}
