using Definition;
using UnityEngine;
using UnityEngine.UI;

public class StateSlider : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [Space(5)]
    [SerializeField]
    private Slider hungryBar;
    [SerializeField]
    private Slider thirstBar;  
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private Slider happinessBar;

    private IUnitService unitService;

    public void Start()
    {
        unitService = target.GetComponent<IUnitService>();
    }


    private void Update()
    {
        hungryBar.value = unitService.GetHungry() / 100f;
        thirstBar.value = unitService.GetThirst() / 100f;
        healthBar.value = unitService.GetHealth() / 100f;
        happinessBar.value = unitService.GetHappiness() / 100f;
    }
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, -target.transform.rotation.y, 0);
    }

}
