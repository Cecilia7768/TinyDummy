using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
public class GameManager : MonoBehaviour
{
    public static UnitService unitService;
    public static IEnumerator GetUnitService(UnitService _unitService)
    {
        while (unitService == null)
        {
            unitService = _unitService;
            yield return null;
        }
    }  

}
