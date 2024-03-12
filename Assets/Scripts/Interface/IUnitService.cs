using UnityEngine.Analytics;

namespace Definition
{
    public interface IUnitService
    {
        public float GetHealth();
        public float GetHungry();
        public float GetThirst();       
        public float GetHappiness();    

        public GenderType GetGender();         

        public float GetMaxHealth();
        public float GetMaxHungry();
        public float GetMaxThirst();
        public float GetPatrolRadius();
        public float GetPatrolTimer();


        public void SetHealth(float health);
        public void SetHungry(float hungry);
        public void SetThirst(float thirst);  
        public void SetHappiness(float happiness);

        /// <summary>
        /// �ູ�� �ʱ�ȭ
        /// </summary>
        public void InitSetHappiness();

        public void SetGender(GenderType genderType);

        public void SetMaxHealth(float health);
        public void SetMaxHungry(float hungry);
        public void SetMaxThirst(float thirst);
        public void SetPatrolRadius(float patrolRadius);
        public void SetPatrolTimer(float patrolTimer);

    }
}