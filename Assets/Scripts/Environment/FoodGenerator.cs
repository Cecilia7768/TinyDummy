using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FoodGenerator : MonoBehaviour
{
    //���� ����
    [SerializeField]
    private GameObject plane;

    [SerializeField]
    [Header("==== FOOD ====")]
    private List<GameObject> foodList = new List<GameObject>();
    [SerializeField]
    private Transform foodParent;

    [SerializeField]
    private const int maxFoodCount = 5; //�����Ǵ� ���� �ִ� ����

    private Queue<GameObject> foodPool = new Queue<GameObject>();

    // CanSee
    public static List<GameObject> createFoodList = new List<GameObject>();

    private bool isFirstActivation = true;

    /// ���� ��� �ֱ�
    private float minReloadTime = 5f;
    private float maxReloadTime = 15f;
    [SerializeField]
    [Header("�׽�Ʈ Ȯ�ο�. �� �����տ� ����� ���� ��� �ֱ�")]
    private float reloadTime;

    private void Awake()
    {
        InitializeFoodPool();
        StartCoroutine(ActivateInitialFood());
        StartCoroutine(SpawnFoodRoutine());
    }

    /// <summary>
    /// ���� Ǯ�� ����
    /// </summary>
    private void InitializeFoodPool()
    {
        foreach (var food in foodList)
        {
            for (int i = 0; i < maxFoodCount / foodList.Count; i++)
            {
                GameObject foodInstance = Instantiate(food, foodParent);
                foodInstance.SetActive(false);
                foodPool.Enqueue(foodInstance);

                createFoodList.Add(foodInstance);
            }
        }
    }
    private IEnumerator ActivateInitialFood()
    {
        while (foodPool.Count > 0)
        {
            float wait = Random.Range(1f, 5f);
            yield return new WaitForSeconds(wait);

            GameObject foodToActivate = foodPool.Dequeue();
            foodToActivate.transform.position = RandomPositionOnPlane();
            foodToActivate.SetActive(true);
        }
        isFirstActivation = false;
    }

    /// <summary>
    /// n�ʸ��� ���� �ڵ� ����.
    /// ���߿� ���� ���� �������� ���� �ֱ� ���߸� �����Ͱ���.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnFoodRoutine()
    {
        reloadTime = Random.Range(minReloadTime, maxReloadTime);
        while (true)
        {
            yield return new WaitForSeconds(reloadTime);

            if (!isFirstActivation && foodParent.childCount < maxFoodCount)
            {
                SpawnFood();
            }
        }
    }

    /// <summary>
    /// Ŭ���� ���� Ǯ�� ���� ����.
    /// </summary>
    /// <returns></returns>
    private IEnumerator OnClickSpawnALLFood() 
    {
        for (int i = 0; i < foodParent.childCount; i++)
        {
            yield return new WaitForSeconds(.5f);
            if (!isFirstActivation && foodParent.childCount < maxFoodCount)
            {
                if (!foodParent.GetChild(i).gameObject.activeSelf)
                {
                    foodParent.GetChild(i).gameObject.SetActive(true);
                    foodParent.GetChild(i).gameObject.transform.position = RandomPositionOnPlane();
                }
            }
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void SpawnFood()
    {
        if (foodPool.Count == 0) return;

        GameObject foodToSpawn = foodPool.Dequeue();
        foodToSpawn.transform.position = RandomPositionOnPlane();
        foodToSpawn.SetActive(true);
    }

    /// <summary>
    /// ���� ���� ���� ����Ʈ
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomPositionOnPlane()
    {
        Renderer planeRenderer = plane.GetComponent<Renderer>();
        Vector3 planeSize = planeRenderer.bounds.size;

        float x = Random.Range(-planeSize.x / 2, planeSize.x / 2);
        float z = Random.Range(-planeSize.z / 2, planeSize.z / 2);
        Vector3 position = plane.transform.position + new Vector3(x, 1, z);

        return position;
    }

    /// <summary>
    /// ������Ʈ ��ġ - �����б�
    /// </summary>
    private void OnMouseDown()
    {
        Debug.LogError(this.gameObject.name);
        StartCoroutine(OnClickSpawnALLFood());
    }
}