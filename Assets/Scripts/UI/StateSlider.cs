using Definition;
using UnityEngine;
using UnityEngine.UI;

public class StateSlider : MonoBehaviour
{
    [SerializeField]
    [Header("머리위 상태바")]
    private GameObject target;

    [SerializeField]
    private Slider hungryBar;
    [SerializeField]
    private Slider thirstBar;

    private IUnitService unitService;

    public void Start()
    {
        unitService = target.GetComponent<IUnitService>();
    }


    private void Update()
    {
        hungryBar.value = unitService.GetHungry() / 100f;
        thirstBar.value = unitService.GetThirst() / 100f;
    }
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, -target.transform.rotation.y, 0);
    }

}
