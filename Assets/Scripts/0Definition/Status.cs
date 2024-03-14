using System;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    [Serializable]
    public struct UnitStatus
    {
        public float health;
        public float hungry;
        public float thirst;
        public float happiness;
        public float ageFigure; //나이 진행도

        public GenderType gender;

        [Space(10)]
        public float maxHealth;
        public float maxHungry;
        public float maxThirst;
        public float maxHappiness;
        public float maxAgeFigure;

        [Space(10)]
        [Header("AI")]
        public float patrolRadius; // 순찰 반경
        public float patrolTimer; // 위치 변경 간격

        public float Health { get { return health; } set { health = value; } }
        public float Hungry { get { return hungry; } set { hungry = value; } }
        public float Thirst { get { return thirst; } set { thirst = value; } }
        public float Happiness { get { return happiness; } set { happiness = value; } }
        public float AgeFigure { get { return ageFigure; } set { ageFigure = value; } }
       
        public GenderType Gender { get { return gender; } set { gender = value; } }        
               
        public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
        public float MaxHungry { get { return maxHungry; } set { maxHungry = value; } }
        public float MaxThirst { get { return maxThirst; } set { maxThirst = value; } }
        public float MaxHappiness { get { return maxHappiness; } set { maxHappiness = value; } }
        public float MaxAgeFigure { get { return maxAgeFigure; } set { maxAgeFigure = value; } }


        public float PatrolRadius { get { return patrolRadius; } set { patrolRadius = value; } }
        public float PatrolTimer { get { return patrolTimer; } set { patrolTimer = value; } }
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

        [Space(5)]
        [Header("성장 상태")]
        public List<GameObject> statePrefabList = new List<GameObject>();

        public IUnitService UnitService { get { return unitService; } set { unitService = value; } }
        public AgeType CurrAge { get { return currAge; } set { currAge = value; } }
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
}