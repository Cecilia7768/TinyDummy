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
        for (int i = 0; i < JjackStandard.FirstCreatCount; i++)
        {
            yield return new WaitForSeconds(.5f);
            jjackTmpObj = Instantiate(jjackPrefab, this.transform);
            jjackTmpObj.transform.localPosition = new Vector3(EnvironmentManager.Instance.nestPosi.position.x
                , 1f, EnvironmentManager.Instance.nestPosi.position.z);
            if (JjackStandard.MaleCount > JjackStandard.FemaleCount)
            {
                jjackTmpObj.GetComponent<UnitService>().unitStatus.Gender = GenderType.Female;
                JjackStandard.FemaleCount++;
            }
            else
            {
                jjackTmpObj.GetComponent<UnitService>().unitStatus.Gender = GenderType.Male;
                JjackStandard.MaleCount++;
            }
            jjackList.Add(jjackTmpObj);
        }
    }
}
