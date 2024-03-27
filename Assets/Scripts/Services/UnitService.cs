using UnityEngine;

namespace Definition
{
    public class UnitService : MonoBehaviour, IUnitService
    {
        public UnitStatus unitStatus;
        private LifeCycleService lifeCycleService;

        private void Awake()
        {
            unitStatus = new UnitStatus();
            lifeCycleService = this.GetComponent<LifeCycleService>();
            InitData();
        }

        #region Interface
        public void InitData()
        {
            AgeType ageType = lifeCycleService.GetCurrAge();
            switch (ageType)
            {
                case AgeType.Egg:
                    int num = 1;
                    if (lifeCycleService.GetUnitService().GetEggGrade() == EggGradeType.Special)
                        num++;
                    unitStatus.MaxHealth = 100f * num;
                    unitStatus.MaxHungry = 100f * num;
                    unitStatus.MaxThirst = 100f * num;
                    unitStatus.MaxHappiness = 100f;
                    unitStatus.MaxAgeFigure = 100f;

                    unitStatus.Health = 80f * num;
                    unitStatus.Hungry = unitStatus.MaxHungry;
                    unitStatus.Thirst = unitStatus.MaxThirst;
                    unitStatus.Happiness = 50f;
                    unitStatus.PatrolRadius = 70f;
                    unitStatus.PatrolTimer = 2f;

                    JjackStandard.EggCount++;
                    break;
                case AgeType.Child:
                    unitStatus.RunAwaySpeed = 5f;
                    JjackStandard.ChildCount++;
                    JjackStandard.EggCount--;
                    break;
                case AgeType.Adult:
                    unitStatus.RunAwaySpeed = 10f;
                    if (unitStatus.Gender == GenderType.Male)
                        JjackStandard.MaleAdultCount++;
                    JjackStandard.AdultCount++;
                    JjackStandard.ChildCount--;
                    break;
                case AgeType.Old:
                    unitStatus.RunAwaySpeed = 5f;
                    if (unitStatus.Gender == GenderType.Male)
                        JjackStandard.MaleAdultCount--;
                    JjackStandard.OldCount++;
                    JjackStandard.AdultCount--;
                    break;
            }
            unitStatus.AgeFigure = 0f;
            unitStatus.IsAttacked = false;

            //성장이벤트 발생위치 초기화
            SetGrowthEventPosi(Vector3.zero);
        }

        //////////////////// GET ////////////////////
        public int GetNum() => unitStatus.Num;
        public float GetHealth() => unitStatus.Health;
        public float GetHungry() => unitStatus.Hungry;
        public float GetThirst() => unitStatus.Thirst;
        public float GetHappiness() => unitStatus.Happiness;
        public float GetAgeFigure() => unitStatus.AgeFigure;

        public bool GetIsFoundEnemy() => unitStatus.IsFoundEnemy;
        public GameObject GetEnemyObj() => unitStatus.EnemyObj;
        public float GetRunAwaySpeed() => unitStatus.RunAwaySpeed;

        public GenderType GetGender() => unitStatus.Gender;
        public EggGradeType GetEggGrade() => unitStatus.EggGrade;

        public float GetMaxHealth() => unitStatus.MaxHealth;
        public float GetMaxHungry() => unitStatus.MaxHungry;
        public float GetMaxThirst() => unitStatus.MaxThirst;
        public float GetMaxAgeFigure() => unitStatus.MaxAgeFigure;
        public bool GetIsAttacked() => unitStatus.IsAttacked;

        public float GetPatrolRadius() => unitStatus.PatrolRadius;
        public float GetPatrolTimer() => unitStatus.PatrolTimer;

        public Vector3 GetGrowthEventPosi() => unitStatus.GrowthEventPosi;

        //////////////////// SET ////////////////////
        public void SetNum(int num) => unitStatus.Num = num;

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

        public void SetAddAgeFigure(float setAge)
        {
            unitStatus.AgeFigure += setAge;
            unitStatus.AgeFigure = Mathf.Clamp(unitStatus.AgeFigure, 0, unitStatus.MaxAgeFigure);
        }

        public void SetIsFoundEnemy(bool isFoundEnemy) => unitStatus.IsFoundEnemy = isFoundEnemy;
        public void SetEnemyObj(GameObject enemyObj) => unitStatus.EnemyObj = enemyObj;
        public void SetRunAwaySpeed(float runAwaySpeed) => unitStatus.RunAwaySpeed = runAwaySpeed;

        public void SetEggGrade(EggGradeType eggGrade) => unitStatus.EggGrade = eggGrade;

        public void InitSetHappiness()
        {
            unitStatus.Happiness = 15;
        }
        public void SetGender(GenderType genderType) => unitStatus.Gender = genderType;

        public void SetMaxHealth(float health) => unitStatus.MaxHealth = health;
        public void SetMaxHungry(float hungry) => unitStatus.MaxHungry = hungry;
        public void SetMaxThirst(float thirst) => unitStatus.MaxThirst = thirst;
        public void SetMaxAgeFigure(float maxAgeFigure) => unitStatus.MaxAgeFigure = maxAgeFigure;
        public bool SetIsAttacked(bool isAttacked) => unitStatus.IsAttacked = isAttacked;

        public void SetPatrolRadius(float patrolRadius) => unitStatus.PatrolRadius = patrolRadius;
        public void SetPatrolTimer(float patrolTimer) => unitStatus.PatrolTimer = patrolTimer;

        public void SetGrowthEventPosi(Vector3 growthEventPosi) => unitStatus.GrowthEventPosi = growthEventPosi;

        #endregion
    }
}