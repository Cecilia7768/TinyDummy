using Definition;
using System;
using System.Collections.Generic;
using UnityEditor;
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
                // 이미 존재하는 인스턴스 검색
                _instance = FindObjectOfType<EnvironmentManager>();

                if (_instance == null)
                {
                    // 게임 오브젝트 생성 후 GameManager 컴포넌트 추가
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
    public Vector3 firstSpawnArea;

    //[Space(3)]
    //[Header("산란 위치")]
    //public Renderer spawnArea;

    /// <summary>
    /// 산란 관련
    /// </summary>
    //public Transform nestPosi;
    public GameObject eggPrefab;

    public static event Action<Vector3> spawnEggEvent;

    private void Start()
    {
        spawnEggEvent += (trans) =>
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
                    if (specialEgg < JjackStandard.ProbabilityBossEgg) // 10% 확률로 우두머리 생성
                    {
                        JjackStandard.BossCount++;
                        CreateEgg(trans, true);
                        return;
                    }
                }
            }

            for (int i = 0; i < eggSpawnCount; i++) 
            {
                CreateEgg(trans);
            }
        };
    }
    public void SpawnEGG(Vector3 trans)
    {
        spawnEggEvent?.Invoke(trans);
    }

    public void CreateEgg(Vector3 trans, bool isSpecialEgg = false)
    {
        GameObject egg = Instantiate(eggPrefab, unitParent);
        egg.transform.position = trans;
        var unitService = egg.GetComponent<UnitService>();

        //우두머리 결정
        unitService.unitStatus.EggGrade = isSpecialEgg ? EggGradeType.Special : EggGradeType.Common;

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

}
