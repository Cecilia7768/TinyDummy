using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    //���� ����
    [SerializeField]
    private GameObject plane;

    [Space(5)]
    [SerializeField]
    [Header("==== Environment ====")]
    private List<GameObject> environmentList = new List<GameObject>();
    [SerializeField]
    private Transform environmentParent;





   
}
