using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
public class UnitManager : MonoBehaviour
{
    [SerializeField]
    private GameObject jjackPrefab;

    [SerializeField]
    private List<GameObject> jjackList = new List<GameObject>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        GameObject jjackTmpObj;
        jjackList.Clear();
        for (int i = 0; i < JjackStandard.jjackMaxCount; i++)
        {
            jjackTmpObj = Instantiate(jjackPrefab, Vector3.zero, Quaternion.identity, this.transform);
            jjackTmpObj.transform.localScale = new Vector3(5, 5, 5);
            jjackList.Add(jjackTmpObj);
        }
    }
}
