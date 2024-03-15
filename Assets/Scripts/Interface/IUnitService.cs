using UnityEngine;

namespace Definition
{
    public interface IUnitService
    {
        public void InitData();


        //////////////////// GET ////////////////////
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

        public Vector3 GetGrowthEventPosi();


        //////////////////// SET ////////////////////
        public void SetHealth(float health);
        public void SetHungry(float hungry);
        public void SetThirst(float thirst);
        public void SetHappiness(float happiness);
        public void SetAddAgeFigure(float setAge);


        /// <summary>
        /// �ູ�� �ʱ�ȭ
        /// </summary>
        public void InitSetHappiness();

        public void SetGender(GenderType genderType);

        public void SetMaxHealth(float health);
        public void SetMaxHungry(float hungry);
        public void SetMaxThirst(float thirst);
        public void SetMaxAgeFigure(float maxAgeFigure);


        public void SetPatrolRadius(float patrolRadius);
        public void SetPatrolTimer(float patrolTimer);

        public void SetGrowthEventPosi(Vector3 growthEventPosi);
    }
}