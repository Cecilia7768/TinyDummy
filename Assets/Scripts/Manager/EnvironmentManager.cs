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
                // �̹� �����ϴ� �ν��Ͻ� �˻�
                _instance = FindObjectOfType<EnvironmentManager>();

                if (_instance == null)
                {
                    // ���� ������Ʈ ���� �� GameManager ������Ʈ �߰�
                    GameObject go = new GameObject("EnvironmentManager");
                    _instance = go.AddComponent<EnvironmentManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    //���� ����
    [SerializeField]
    private GameObject plane;

    [Space(3)]
    [SerializeField]
    [Header("==== ���� ���� ��ġ ====")]
    private Transform unitParent;

    [Space(3)]
    [Header("ù ���� ��ġ")]
    public Vector3 firstSpawnArea;

    //[Space(3)]
    //[Header("��� ��ġ")]
    //public Renderer spawnArea;

    /// <summary>
    /// ��� ����
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
            if (doubleSpawnChance < JjackStandard.ProbabilityDoubleEgg) // 20% Ȯ���� �� ���� egg ����
            {
                eggSpawnCount = 2;
            }
            else
            {
                if (JjackStandard.BossCount == 0)
                {
                    int specialEgg = UnityEngine.Random.Range(0, 100);
                    if (specialEgg < JjackStandard.ProbabilityBossEgg) // 10% Ȯ���� ��θӸ� ����
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

        //��θӸ� ����
        unitService.unitStatus.EggGrade = isSpecialEgg ? EggGradeType.Special : EggGradeType.Common;

        DetermineGenderForEgg(unitService);

        // ���� ���� ����
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
