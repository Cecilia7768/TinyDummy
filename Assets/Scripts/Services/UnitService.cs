using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    public class UnitService : MonoBehaviour, IUnitService
    {
        public UnitStatus unitStatus;

        private void Start()
        {
            StartCoroutine(GameManager.GetUnitService(this.GetComponent<UnitService>()));
        }

        #region Interface
        public float GetHealth() => unitStatus.Health;
        public float GetHungry() => unitStatus.Hungry;
        public float GetThirst() => unitStatus.Thirst;
        public float GetMaxHealth() => unitStatus.MaxHealth;
        public float GetMaxHungry() => unitStatus.MaxHungry;
        public float GetMaxThirst() => unitStatus.MaxThirst;
        public float GetPatrolRadius() => unitStatus.PatrolRadius;
        public float GetPatrolTimer() => unitStatus.PatrolTimer;


        public void SetHealth(float health)
        {
            unitStatus.Health += health; 
            if(unitStatus.Health > unitStatus.MaxHealth)
                unitStatus.Health = unitStatus.MaxHealth;
            else if(unitStatus.Health < 0)
                unitStatus.Health = 0;
        }
        public void SetHungry(float hungry)
        {
            unitStatus.Hungry += hungry;
            if (unitStatus.Hungry > unitStatus.maxHungry)
                unitStatus.Hungry = unitStatus.maxHungry;
            else if (unitStatus.Hungry < 0)
                unitStatus.Hungry = 0;
        }
        public void SetThirst(float thirst)
        {
            unitStatus.Thirst += thirst;
            if (unitStatus.Thirst > unitStatus.MaxThirst)
                unitStatus.Thirst = unitStatus.MaxThirst;
            else if (unitStatus.Thirst < 0)
                unitStatus.Thirst = 0;
        }        


        public void SetMaxHealth(float health) => unitStatus.MaxHealth = health;
        public void SetMaxHungry(float hungry) => unitStatus.MaxHungry = hungry;
        public void SetMaxThirst(float thirst) => unitStatus.MaxThirst = thirst;
        public void SetPatrolRadius(float patrolRadius) => unitStatus.PatrolRadius = patrolRadius;
        public void SetPatrolTimer(float patrolTimer) => unitStatus.PatrolTimer = patrolTimer; 

        #endregion
    }
}