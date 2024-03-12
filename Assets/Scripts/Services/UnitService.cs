using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Definition
{
    public class UnitService : MonoBehaviour, IUnitService
    {
        public UnitStatus unitStatus;

        #region Interface

        public float GetHealth() => unitStatus.Health;
        public float GetHungry() => unitStatus.Hungry;
        public float GetThirst() => unitStatus.Thirst;
        public float GetHappiness() => unitStatus.Happiness;

        public GenderType GetGender() => unitStatus.Gender;

        public float GetMaxHealth() => unitStatus.MaxHealth;
        public float GetMaxHungry() => unitStatus.MaxHungry;
        public float GetMaxThirst() => unitStatus.MaxThirst;
        public float GetPatrolRadius() => unitStatus.PatrolRadius;
        public float GetPatrolTimer() => unitStatus.PatrolTimer;


        public void SetHealth(float health)
        {
            this.unitStatus.Health += health; 
            unitStatus.Health = Mathf.Clamp(unitStatus.Health, 0, unitStatus.MaxHealth);
        }
        public void SetHungry(float hungry)
        {
            unitStatus.Hungry += hungry;
            unitStatus.Hungry = Mathf.Clamp(unitStatus.Hungry, 0, unitStatus.MaxHungry);
        }
        public void SetThirst(float thirst)
        {
            unitStatus.Thirst += thirst;
            unitStatus.Thirst = Mathf.Clamp(unitStatus.Thirst, 0, unitStatus.MaxThirst);
        }

        public void SetHappiness(float happiness)
        {
            unitStatus.Happiness += happiness;
            unitStatus.Happiness = Mathf.Clamp(unitStatus.Happiness, 0, unitStatus.MaxHappiness);
        }

        public void InitSetHappiness()
        {
            unitStatus.Happiness = 50;
        }
        public void SetGender(GenderType genderType) => unitStatus.Gender = genderType;

        public void SetMaxHealth(float health) => unitStatus.MaxHealth = health;
        public void SetMaxHungry(float hungry) => unitStatus.MaxHungry = hungry;
        public void SetMaxThirst(float thirst) => unitStatus.MaxThirst = thirst;
        public void SetPatrolRadius(float patrolRadius) => unitStatus.PatrolRadius = patrolRadius;
        public void SetPatrolTimer(float patrolTimer) => unitStatus.PatrolTimer = patrolTimer;

        #endregion
    }
}