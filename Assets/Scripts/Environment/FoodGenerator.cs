using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FoodGenerator : MonoBehaviour
{
    //생성 범위
    [SerializeField]
    private GameObject plane;

    [SerializeField]
    [Header("==== FOOD ====")]
    private List<GameObject> foodList = new List<GameObject>();
    [SerializeField]
    private Transform foodParent;
    [SerializeField]
    private int maxFoodCount = 10;

    private Queue<GameObject> foodPool = new Queue<GameObject>();

    // CanSee
    public static List<GameObject> createFoodList = new List<GameObject>();

    private bool isFirstActivation = true;

    private void Awake()
    {
        InitializeFoodPool();
        StartCoroutine(ActivateInitialFood());
        StartCoroutine(SpawnFoodRoutine());
    }

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
            float wait = Random.Range(.5f, 2f);
            yield return new WaitForSeconds(wait);

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

            if (!isFirstActivation && foodParent.childCount < maxFoodCount)
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
        Vector3 position = plane.transform.position + new Vector3(x, 1, z);

        return position;
    }
}