using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField]
    private GameObject plane;
    [SerializeField]
    private List<GameObject> foodList = new List<GameObject>();
    [SerializeField]
    private Transform createParent;
    [SerializeField]
    private int maxFoodCount = 10;

    private Queue<GameObject> foodPool = new Queue<GameObject>();
    private bool isFirstActivation = true;

    // CanSee
    public static List<GameObject> createFoodList = new List<GameObject>();

    private void Awake()
    {
        InitializeFoodPool();
        ActivateInitialFood();
        StartCoroutine(SpawnFoodRoutine());
    }

    private void InitializeFoodPool()
    {
        foreach (var food in foodList)
        {
            for (int i = 0; i < maxFoodCount / foodList.Count; i++)
            {
                GameObject foodInstance = Instantiate(food, createParent);
                foodInstance.SetActive(false);
                foodPool.Enqueue(foodInstance);

                createFoodList.Add(foodInstance);
            }
        }
    }

    private void ActivateInitialFood()
    {
        while (foodPool.Count > 0)
        {
            GameObject foodToActivate = foodPool.Dequeue();
            foodToActivate.transform.position = RandomPositionOnPlane();
            foodToActivate.SetActive(true);
        }
        isFirstActivation = false;
    }

    private IEnumerator SpawnFoodRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            if (!isFirstActivation && createParent.childCount < maxFoodCount)
            {
                SpawnFood();
            }
        }
    }

    private void SpawnFood()
    {
        if (foodPool.Count == 0) return;

        GameObject foodToSpawn = foodPool.Dequeue();
        foodToSpawn.transform.position = RandomPositionOnPlane();
        foodToSpawn.SetActive(true);
    }

    private Vector3 RandomPositionOnPlane()
    {
        Renderer planeRenderer = plane.GetComponent<Renderer>();
        Vector3 planeSize = planeRenderer.bounds.size;

        float x = Random.Range(-planeSize.x / 2, planeSize.x / 2);
        float z = Random.Range(-planeSize.z / 2, planeSize.z / 2);
        Vector3 position = plane.transform.position + new Vector3(x, 0, z);

        return position;
    }
}
