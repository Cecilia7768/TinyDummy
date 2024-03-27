using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    #region Singleton
    private static EnvironmentManager _instance;

    public static EnvironmentManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnvironmentManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("EnvironmentManager");
                    _instance = go.AddComponent<EnvironmentManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    //생성 범위
    [SerializeField]
    private GameObject plane;

    [Space(3)]
    [SerializeField]
    [Header("==== 유닛 생성 위치 ====")]
    private Transform unitParent;

    [Space(3)]
    [Header("첫 스폰 위치")]
    public Renderer firstSpawnArea;

    //[Space(3)]
    //[Header("산란 위치")]
    //public Renderer spawnArea;

    /// <summary>
    /// 산란 관련
    /// </summary>
    //public Transform nestPosi;

    [SerializeField]
    [Header("유닛 프리팹")]
    private GameObject unitPrefab;

    //유닛 딕셔너리
    [SerializeField]
    private static Dictionary<int, GameObject> unitsDic = new Dictionary<int, GameObject>();
    public static Dictionary<int, GameObject> UnitsDic { get { return unitsDic; } }

    private void Awake()
    {
        GameManager.firstGameStartSetInit += () => StartCoroutine(CreateFirstUnits());
    }

    /// <summary>
    /// 게임 시작시 기본 첫 유닛 생성
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateFirstUnits()
    {
        GameObject jjackTmpObj;
        for (int i = 0; i < JjackStandard.FirstCreatCount; i++)
        {
            yield return new WaitForSeconds(.5f);
            jjackTmpObj = Instantiate(unitPrefab, unitParent);

            if (JjackStandard.MaleCount > JjackStandard.FemaleCount)
            {
                jjackTmpObj.GetComponent<UnitService>().SetGender(GenderType.Female);
                JjackStandard.FemaleCount++;
            }
            else
            {
                jjackTmpObj.GetComponent<UnitService>().SetGender(GenderType.Male);
                JjackStandard.MaleCount++;
            }

            jjackTmpObj.transform.GetChild(0).localPosition = SetFirstCreateRandomPosition();

            jjackTmpObj.GetComponent<UnitService>().SetNum(JjackStandard.UnitNum);
            unitsDic.Add(i, jjackTmpObj);
            JjackStandard.TotalCount++;
            JjackStandard.UnitNum++;
        }
    }

    /// <summary>
    /// 둥지 내 산란할 위치
    /// </summary>
    /// <returns></returns>
    private Vector3 SetFirstCreateRandomPosition()
    {
        Renderer planeRenderer = firstSpawnArea;
        Vector3 planeSize = planeRenderer.bounds.size;

        float x = UnityEngine.Random.Range(-planeSize.x / 2, planeSize.x / 2);
        float z = UnityEngine.Random.Range(-planeSize.z / 2, planeSize.z / 2);
        Vector3 position = firstSpawnArea.transform.position + new Vector3(x, 1, z);

        return position;
    }

    /// <summary>
    /// 산란 시스템
    /// </summary>
    /// <param name="trans"></param>
    public void SpawnEGG(Vector3 trans)
    {
        int eggSpawnCount = 1;
        int doubleSpawnChance = UnityEngine.Random.Range(0, 100);
        if (doubleSpawnChance < JjackStandard.ProbabilityDoubleEgg) // 20% 확률로 두 개의 egg 생성
        {
            eggSpawnCount = 2;
        }
        else
        {
            if (JjackStandard.BossCount == 0)
            {
                int specialEgg = UnityEngine.Random.Range(0, 100);
                if (specialEgg < JjackStandard.ProbabilityBossEgg &&
                    JjackStandard.BossCount < JjackStandard.BossMaxCount) // 10% 확률로 우두머리 생성
                {
                    JjackStandard.BossCount++;
                    CreateEgg(trans, true);
                    return;
                }
            }
        }

        for (float i = 0, posi = 0; i < eggSpawnCount; i++, posi += .2f)
        {
            CreateEgg(new Vector3(trans.x + posi, 0, trans.z + posi));
        }
    }

    public void CreateEgg(Vector3 trans, bool isSpecialEgg = false)
    {
        GameObject egg = Instantiate(unitPrefab, unitParent);
        egg.transform.GetChild(0).localPosition = trans;

        egg.GetComponent<UnitService>().SetNum(JjackStandard.UnitNum);
        unitsDic.Add(JjackStandard.UnitNum, egg);
        JjackStandard.TotalCount++;
        JjackStandard.UnitNum++;

        var unitService = egg.GetComponent<UnitService>();

        //우두머리 결정
        unitService.unitStatus.EggGrade = isSpecialEgg ? EggGradeType.Special : EggGradeType.Common;

        if(isSpecialEgg)
        {
            int num = Random.Range(0, BossData.BossAbilityList.Count);
            foreach(var data in BossData.BossAbilityList)
            {
                if(data.Key == num)
                {
                    data.Value?.Invoke();
                    break;
                }
            }
        }

        DetermineGenderForEgg(unitService);

        // 성별 결정 로직
        void DetermineGenderForEgg(UnitService service)
        {
            bool isBalance = JjackStandard.FemaleCount == JjackStandard.MaleCount;
            float probabilityToBeFemale = isBalance ? 0.5f : (JjackStandard.MaleCount > JjackStandard.FemaleCount ? 0.6f : 0.4f);

            if (UnityEngine.Random.Range(0f, 1f) < probabilityToBeFemale)
            {
                service.unitStatus.Gender = GenderType.Female;
                JjackStandard.FemaleCount++;
            }
            else
            {
                service.unitStatus.Gender = GenderType.Male;
                JjackStandard.MaleCount++;
            }
        }
    }

    /// <summary>
    /// 테스트용 우두머리 생성 버튼
    /// </summary>
    public void MakeBossUnit()
    {
        CreateEgg(SetFirstCreateRandomPosition(), true);
        JjackStandard.BossCount++;
    }
}
