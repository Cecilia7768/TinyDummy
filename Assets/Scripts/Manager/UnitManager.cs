using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitManager : MonoBehaviour
{
    [SerializeField]
    private GameObject jjackPrefab;

    private void Start()
    {
        StartCoroutine(Init());
    }

    /// <summary>
    /// 게임 시작시 기본 첫 유닛 생성
    /// </summary>
    /// <returns></returns>
    IEnumerator Init()
    {
        GameObject jjackTmpObj;
        for (int i = 0; i < JjackStandard.FirstCreatCount; i++)
        {
            yield return new WaitForSeconds(1f);
            jjackTmpObj = Instantiate(jjackPrefab, this.transform);
            jjackTmpObj.transform.localPosition = new Vector3(EnvironmentManager.Instance.firstSpawnArea.x
                , 1f, EnvironmentManager.Instance.firstSpawnArea.z);

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
        }
    }

    //private UnityEngine.Vector3 SetRandomPosition()
    //{
    //    Renderer planeRenderer = EnvironmentManager.Instance.firstSpawnArea;
    //    UnityEngine.Vector3 planeSize = planeRenderer.bounds.size;

    //    float x = Random.Range(-planeSize.x / 2, planeSize.x / 2);
    //    float z = Random.Range(-planeSize.z / 2, planeSize.z / 2);
    //    UnityEngine.Vector3 position = EnvironmentManager.Instance.firstSpawnArea.transform.position + new UnityEngine.Vector3(x, 1, z);

    //    return position;
    //}
}
