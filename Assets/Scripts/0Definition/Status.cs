using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Definition
{
    [Serializable]
    public struct UnitStatus
    {
        public int num;

        public float health;
        public float hungry;
        public float thirst;
        public float happiness;
        public float ageFigure; //���� ���൵

        ///////// õ�� ���� /////////
        [Space(5)]
        public bool isFoundEnemy; //�� �߰�
        public GameObject enemyObj; //�߰��� �� ������Ʈ
        public float runAwaySpeed;

        [Space(5)]
        public GenderType gender;
        public EggGradeType eggGrade;

        [Space(10)]
        public float maxHealth;
        public float maxHungry;
        public float maxThirst;
        public float maxHappiness;
        public float maxAgeFigure;

        [Space(10)]
        public bool isAttacked; //��Ƹ����� �ִ��� ����

        [Space(3)]
        [Header("AI")]
        public float patrolRadius; // ���� �ݰ�
        public float patrolTimer; // ��ġ ���� ����

        [Space(3)]
        [Header("Position")]
        public Vector3 growthEventPosi; //�����̺�Ʈ �߻���ġ.

        public int Num { get { return num; } set { num = value; } }

        public float Health { get { return health; } set { health = value; } }
        public float Hungry { get { return hungry; } set { hungry = value; } }
        public float Thirst { get { return thirst; } set { thirst = value; } }
        public float Happiness { get { return happiness; } set { happiness = value; } }
        public float AgeFigure { get { return ageFigure; } set { ageFigure = value; } }

        public bool IsFoundEnemy { get { return isFoundEnemy; } set { isFoundEnemy = value; } }
        public GameObject EnemyObj { get { return enemyObj; } set { enemyObj = value; } }
        public float RunAwaySpeed { get { return runAwaySpeed; } set { runAwaySpeed = value; } }

        public GenderType Gender { get { return gender; } set { gender = value; } }
        public EggGradeType EggGrade { get { return eggGrade; } set { eggGrade = value; } }

        public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
        public float MaxHungry { get { return maxHungry; } set { maxHungry = value; } }
        public float MaxThirst { get { return maxThirst; } set { maxThirst = value; } }
        public float MaxHappiness { get { return maxHappiness; } set { maxHappiness = value; } }
        public float MaxAgeFigure { get { return maxAgeFigure; } set { maxAgeFigure = value; } }
       
        public bool IsAttacked { get { return isAttacked; } set { isAttacked = value; } }

        public float PatrolRadius { get { return patrolRadius; } set { patrolRadius = value; } }
        public float PatrolTimer { get { return patrolTimer; } set { patrolTimer = value; } }
        public Vector3 GrowthEventPosi { get { return growthEventPosi; } set { growthEventPosi = value; } }
    }

    [Serializable]
    public struct ObjectStatus
    {
        public float health;
        public float hungry;
        public float thirst;

        public FoodType foodType;

        public float Health { get { return health; } set { health = value; } }
        public float Hungry { get { return hungry; } set { hungry = value; } }
        public float Thirst { get { return thirst; } set { thirst = value; } }
        public FoodType FoodType { get { return foodType; } set { foodType = value; } }
    }

    [Serializable]
    public class LifeCycleStatus
    {
        public IUnitService unitService;
        public AgeType currAge;
        public LifeCycleService lifeCycleService;

        [Space(5)]
        [Header("���� ����")]
        public List<GameObject> statePrefabList = new List<GameObject>();

        public IUnitService UnitService { get { return unitService; } set { unitService = value; } }
        public AgeType CurrAge { get { return currAge; } set { currAge = value; } }
        public LifeCycleService LifeCycleService { get { return lifeCycleService; } set { lifeCycleService = value; } }
        public List<GameObject> StatePrefabList { get { return statePrefabList; } set { statePrefabList = value; } }
    }

    public enum FoodType
    {
        None = -1,
        Food,
        Drink,
    }

    public enum GenderType
    {
        None = -1,
        Male,
        Female,
    }
    public enum AgeType
    {
        None = -1,
        Egg,
        Child,
        Adult,
        Old,
        Dead,
    }
    public enum EggGradeType
    {
        None = -1,
        Common,
        Special,
    }

    /////////////////////////// Enemy ///////////////////////////

    [Serializable]
    public struct EnemyStatus
    {
        public int num;

        public GameObject target;
        public Vector3 targetPosi;

        [Space(10)]
        public float moveSpeed;
        public float attackPower;
        public float attackSpeed;

        public bool isCanEat;
        public bool isCanHunt;

        public int huntingProgress; //��� ���൵
        public int maxHuntingProgress; //��� �������.

        [Space(10)]
        [Header("AI")]
        public float patrolRadius; // ���� �ݰ�
        public float patrolTimer; // ��ġ ���� ����

        public int Num { get { return num; } set { num = value; } }

        public GameObject Target { get { return target; } set { target = value; } }
        public Vector3 TargetPosi { get { return targetPosi; } set { targetPosi = value; } }
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
        public float AttackPower { get { return attackPower; } set { attackPower = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        
        public bool IsCanEat { get { return isCanEat; } set { isCanEat = value; } }
        public bool IsCanHunt { get { return isCanHunt; } set { isCanHunt = value; } }
               
        public int HuntingProgress { get { return huntingProgress; } set { huntingProgress = value; } }
        public int MaxHuntingProgress { get { return maxHuntingProgress; } set { maxHuntingProgress = value; } }

        public float PatrolRadius { get { return patrolRadius; } set { patrolRadius = value; } }
        public float PatrolTimer { get { return patrolTimer; } set { patrolTimer = value; } }
    }
}