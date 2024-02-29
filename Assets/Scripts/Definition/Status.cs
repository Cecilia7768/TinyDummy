using System;
using UnityEngine;

namespace Definition
{
    [Serializable]
    public struct UnitStatus
    {
        public float health;
        public float hungry;
        public float thirst;
        [Space(10)]
        public float maxHealth;
        public float maxHungry;
        public float maxThirst;
        [Space(10)]
        [Header("AI")]
        public float patrolRadius; // ���� �ݰ�
        public float patrolTimer; // ��ġ ���� ����

        public float Health { get { return health; } set { health = value; } }
        public float Hungry { get { return hungry; } set { hungry = value; } }
        public float Thirst { get { return thirst; } set { thirst = value; } }
               
        public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
        public float MaxHungry { get { return maxHungry; } set { maxHungry = value; } }
        public float MaxThirst { get { return maxThirst; } set { maxThirst = value; } }


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

    public enum FoodType
    {
        None = -1,
        Food,
        Drink,
    }

}