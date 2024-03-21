using Definition;
using System;
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

    [Space(5)]
    [SerializeField]
    [Header("==== Environment ====")]
    private List<GameObject> environmentList = new List<GameObject>();
    [SerializeField]
    private Transform environmentParent;

    [Space(3)]
    [SerializeField]
    [Header("==== ���� ���� ��ġ ====")]
    private Transform unitParent;


    /// <summary>
    /// ��� ����
    /// </summary>
    public Transform nestPosi;
    public GameObject eggPrefab;

    public static event Action spawnEgg;

    private void Start()
    {
        spawnEgg += () =>
        {
            GameObject egg = Instantiate(eggPrefab, unitParent);
            int ran = UnityEngine.Random.Range(0, 100);
            if (ran % 2 == 0)
            {
                egg.GetComponent<UnitService>().unitStatus.Gender = GenderType.Female;
                JjackStandard.FemaleCount++;
            }
            else
            {
                egg.GetComponent<UnitService>().unitStatus.Gender = GenderType.Male;
                JjackStandard.MaleCount++;
            }
        };
    }

    public void SpawnEGG()
    {
        spawnEgg?.Invoke();
    }
}
