using Definition;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

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
    [SerializeField]
    private Slider ageFigureBar;

    private ILifeCycleService iLifeCycleService;  

    private void Update()
    {
        if (iLifeCycleService == null)
            iLifeCycleService = this.transform.parent.GetComponent<LifeCycle>().iLifeCycleService;
        else
        {
            if (this.gameObject.activeSelf && iLifeCycleService.GetCurrAge() != AgeType.Egg)
            {
                hungryBar.value = iLifeCycleService.GetUnitService().GetHungry() / 100f;
                thirstBar.value = iLifeCycleService.GetUnitService().GetThirst() / 100f;
                healthBar.value = iLifeCycleService.GetUnitService().GetHealth() / 100f;
                happinessBar.value = iLifeCycleService.GetUnitService().GetHappiness() / 100f;
                ageFigureBar.value = iLifeCycleService.GetUnitService().GetAgeFigure() /
                    iLifeCycleService.GetUnitService().GetMaxAgeFigure();
            }
        }
    }
    void LateUpdate()
    {
        if (iLifeCycleService != null)
        {
            if (this.gameObject.activeSelf && iLifeCycleService.GetCurrAge() != AgeType.Egg)
                transform.rotation = Quaternion.Euler(0, -target.transform.rotation.y, 0);
        }
    }
}
