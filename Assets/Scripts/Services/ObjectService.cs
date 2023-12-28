using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Definition
{
    public class ObjectService : MonoBehaviour, IObjectService
    {
        public ObjectStatus objectStatus;

        #region Interface
        public float GetHealth() => objectStatus.Health;
        public float GetHungry() => objectStatus.Hungry;
        public float GetThirst() => objectStatus.Thirst;

        public FoodType GetFoodType() => objectStatus.foodType;

        //public void SetHealth(float health) => objectStatus.Health = health;
        //public void SetHungry(float hungry) => objectStatus.Hungry = hungry;
        //public void SetThirst(float thirst) => objectStatus.Thirst = thirst;
        #endregion
    }
}