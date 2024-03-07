using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitManager : MonoBehaviour
{
    [SerializeField]
    private GameObject jjackPrefab;

    [SerializeField]
    private List<GameObject> jjackList = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        GameObject jjackTmpObj;
        jjackList.Clear();
        for (int i = 0; i < JjackStandard.jjackMaxCount; i++)
        {
            yield return new WaitForSeconds(.5f);
            jjackTmpObj = Instantiate(jjackPrefab, Vector3.zero, Quaternion.identity, this.transform);
            jjackTmpObj.transform.localScale = new Vector3(5, 5, 5);
            jjackTmpObj.transform.position = new Vector3(0, 25.5f, 0);
            if (JjackStandard.maleCount > JjackStandard.femaleCount)
            {
                jjackTmpObj.GetComponent<UnitService>().unitStatus.Gender = GenderType.Female;
                JjackStandard.femaleCount++;
            }
            else
            {
                jjackTmpObj.GetComponent<UnitService>().unitStatus.Gender = GenderType.Male;
                JjackStandard.maleCount++;
            }

            jjackList.Add(jjackTmpObj);
        }
    }
}
