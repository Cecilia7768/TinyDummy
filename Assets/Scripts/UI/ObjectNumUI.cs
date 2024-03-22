using Definition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectNumUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text eggNum;
    [SerializeField]
    private TMP_Text childNum;
    [SerializeField]
    private TMP_Text adultNum;
    [SerializeField]
    private TMP_Text oldNum;
    [SerializeField]
    private TMP_Text deadNum;

    [SerializeField]
    private TMP_Text maleNum;
    [SerializeField]
    private TMP_Text femaleNum;

    [SerializeField]
    private GameObject specialUnit;

    private void Start()
    {
        StartCoroutine(SetObjectNum());
    }

    IEnumerator SetObjectNum()
    {
        while(true)
        {
            yield return null;

            eggNum.text = JjackStandard.EggCount.ToString();
            childNum.text = JjackStandard.ChildCount.ToString();    
            adultNum.text = JjackStandard.AdultCount.ToString();
            oldNum.text = JjackStandard.OldCount.ToString();
            deadNum.text = JjackStandard.DeadCount.ToString();

            maleNum.text = JjackStandard.MaleCount.ToString();
            femaleNum.text = JjackStandard.FemaleCount.ToString();

            specialUnit.SetActive(JjackStandard.BossCount > 0);
        }
    }
}
