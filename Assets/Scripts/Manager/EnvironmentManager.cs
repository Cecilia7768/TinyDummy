using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Movement;
using System;
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
            GameObject egg = Instantiate(eggPrefab, nestPosi);
            egg.transform.position = new Vector3 (nestPosi.position.x, 1f,
                nestPosi.position.z);
        };
    }

    public void SpawnEGG()
    {
        spawnEgg?.Invoke();
    }
}
