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
            StartCoroutine(SetHealthCondition());
            StartCoroutine(SetHappinessCondition());
        }

        /// <summary>
        /// 건강 수치
        /// </summary>
        /// <returns></returns>
        IEnumerator SetHealthCondition()
        {
            while (true)
            {
                if (unitStatus.Hungry >= 80 && unitStatus.Thirst >= 80)              
                    unitStatus.Health += 1;           
                else if(unitStatus.Hungry <= 0 || unitStatus.Thirst <= 0)
                    unitStatus.Health -= 1;

                yield return new WaitForSeconds(1f); 
            }
        }

        /// <summary>
        /// 행복 수치
        /// </summary>
        /// <returns></returns>
        IEnumerator SetHappinessCondition()
        {
            while (true)
            {
                if (unitStatus.Health >= 80)
                {
                    unitStatus.Happiness += 1;
                }

                yield return new WaitForSeconds(2f);
            }
        }


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
            if(this.unitStatus.Health > unitStatus.MaxHealth)
                this.unitStatus.Health = unitStatus.MaxHealth;
            else if(this.unitStatus.Health < 0)
                this.unitStatus.Health = 0;
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

        public void SetHappiness(float happiness)
        {
            unitStatus.Happiness += happiness;
            if(unitStatus.Happiness > unitStatus.MaxHappiness)
                unitStatus.Happiness = unitStatus.MaxHappiness;
            else if (unitStatus.Happiness < 0)
                unitStatus.Happiness = 0;
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