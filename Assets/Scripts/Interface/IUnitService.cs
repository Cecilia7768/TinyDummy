using UnityEngine;

namespace Definition
{
    public interface IUnitService
    {
        public void InitData();

        //////////////////// GET ////////////////////
        public int GetNum();

        public float GetHealth();
        public float GetHungry();
        public float GetThirst();
        public float GetHappiness();
        public float GetAgeFigure();

        public GenderType GetGender();
        public EggGradeType GetEggGrade();

        public float GetMaxHealth();
        public float GetMaxHungry();
        public float GetMaxThirst();
        public float GetMaxAgeFigure();

        public bool GetIsAttacked();

        public float GetPatrolRadius();
        public float GetPatrolTimer();

        public Vector3 GetGrowthEventPosi();


        //////////////////// SET ////////////////////
        public void SetNum(int health);

        public void SetHealth(float health);
        public void SetHungry(float hungry);
        public void SetThirst(float thirst);
        public void SetHappiness(float happiness);
        public void SetAddAgeFigure(float setAge);

        public void SetEggGrade(EggGradeType eggGrade);

        /// <summary>
        /// 행복도 초기화
        /// </summary>
        public void InitSetHappiness();

        public void SetGender(GenderType genderType);

        public void SetMaxHealth(float health);
        public void SetMaxHungry(float hungry);
        public void SetMaxThirst(float thirst);
        public void SetMaxAgeFigure(float maxAgeFigure);

        public bool SetIsAttacked(bool isAttacked);
        public void SetPatrolRadius(float patrolRadius);
        public void SetPatrolTimer(float patrolTimer);

        public void SetGrowthEventPosi(Vector3 growthEventPosi);
    }
}