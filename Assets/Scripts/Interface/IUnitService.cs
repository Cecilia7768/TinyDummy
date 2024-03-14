using UnityEditor;
using UnityEngine.Analytics;

namespace Definition
{
    public interface IUnitService
    {
        public float GetHealth();
        public float GetHungry();
        public float GetThirst();       
        public float GetHappiness();    
        public float GetAgeFigure();    

        public GenderType GetGender();

        public float GetMaxHealth();
        public float GetMaxHungry();
        public float GetMaxThirst();
        public float GetMaxAgeFigure();

        public float GetPatrolRadius();
        public float GetPatrolTimer();


        public void SetHealth(float health);
        public void SetHungry(float hungry);
        public void SetThirst(float thirst);  
        public void SetHappiness(float happiness);
        public void SetAgeFigure(float setAge);


        /// <summary>
        /// 행복도 초기화
        /// </summary>
        public void InitSetHappiness();

        public void SetGender(GenderType genderType);

        public void SetMaxHealth(float health);
        public void SetMaxHungry(float hungry);
        public void SetMaxThirst(float thirst);
        public void SetMaxAgeFigure(float maxAgeFigure);


        public void SetPatrolRadius(float patrolRadius);
        public void SetPatrolTimer(float patrolTimer);

    }
}