using Definition;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StateSlider : LifeCycle
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



    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            hungryBar.value = UnitService.GetHungry() / 100f;
            thirstBar.value = UnitService.GetThirst() / 100f;
            healthBar.value = UnitService.GetHealth() / 100f;
            happinessBar.value = UnitService.GetHappiness() / 100f;
        }
    }
    void LateUpdate()
    {
        if (this.gameObject.activeSelf)
            transform.rotation = Quaternion.Euler(0, -target.transform.rotation.y, 0);
    }
}
